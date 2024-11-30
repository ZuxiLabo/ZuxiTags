using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ZuxiTags
{
    internal class Server
    {

        WebClient webClient;
        Dictionary<string, TagData> userIdToTags = new Dictionary<string, TagData>();



        internal string getUserByName(string name)
        {
            string aabc = webClient.DownloadString("getuserbyname?name=" + name);
            Debug.WriteLine(aabc);
            return aabc;
        }

        internal VRCPlayer GetVRCPlayerbyName(string name)
        {
            Debug.WriteLine(name);
            return PlayerManager.GetUserFromJSON(getUserByName(HttpUtility.UrlEncode(name)), name);
        }

        internal void GetUserTagByID(string id,string UserName, Action<bool, TagData> OnComplete)
        {
            idQueue.Enqueue(new Request() { userId = id, onComplete = OnComplete, userName = UserName });
        }

        private void FetchUserTag(string id,string username, Action<bool, TagData> OnComplete)
        {
            
            if (!IsValidUserID(id))
                OnComplete(false, null);
            if (userIdToTags.TryGetValue(id, out TagData tagData))
            {
                OnComplete(true, tagData);
                return;
            }

            try
            {
                
                
                var jsonString = webClient.DownloadString($"/api/v7/vrchat/getusertag?id={id}&un={username}");
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<TagData>(jsonString);

                if (!string.IsNullOrEmpty(data?.error) && !data.error.Contains("not found"))
                {
                    LogManager.Log("Request to lookup user with tag failed with error: " + data.error);
                    OnComplete(false, null);  // Return false if there's an error in the data
                }
                data.username = username;
                data.userId = id;
                userIdToTags[id] = data;
                OnComplete(true, data);  // Return true if the data is retrieved successfully
            }

            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorMessage = reader.ReadToEnd();
                    if (errorMessage.Contains("<html"))
                    {
                        LogManager.Log("Request to lookup user with tag failed with error: Server Offline Possible");
                        OnComplete(false, null);
                    }

                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<TagData>(errorMessage);
                   
                    if (!string.IsNullOrEmpty(data?.error) && !data.error.Contains("not found"))
                    {
                        LogManager.Log("lookup user failed with error: " + data.error);
                        OnComplete(false, null);  // Return false if there's an error in the data
                    }

                }
                OnComplete(false, null);  // Return false if the request results in a 404
            }
            catch (Exception ex)
            {
                ///   LogManager.Log("Request failed: " + ex.Message);
                OnComplete(false, null);  // Return false for other exceptions
            }
        }
        internal void CreatePrams()
        {
       
        }
        public Server()
        {
            webClient = new WebClient();
            webClient.BaseAddress = "https://api.zuxi.dev";
            webClient.Headers.Add(HttpRequestHeader.UserAgent, "ZuxiProject.VRCTags.ce5e4893");
           
            StartAutoLoader();

        }


        private Queue<Request> idQueue = new();
        private DateTime lastRequestTime = DateTime.MinValue;
        private const int REQUEST_LIMIT = 10;
        private const int TIME_WINDOW_SECONDS = 10;
        private const int CACHE_DURATION_SECONDS = 5;


        private void StartAutoLoader()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (idQueue.Count > 4)
                    {
                        LogManager.Log("Waiting 10 Seconds Before Processing Next Batch");
                        await Task.Delay(10000); // 10 seconds wait
                    }

                    int processedRequests = 0;
                    DateTime startTime = DateTime.Now;

                    while (idQueue.Count > 0 && processedRequests < REQUEST_LIMIT)
                    {
                        Request data = idQueue.Dequeue();

                        try
                        {
                            // Process the request
                            FetchUserTag(data.userId, data.userName, data.onComplete);
                            
                           
                        }
                        catch (Exception ex)
                        {
                            idQueue.Enqueue(data);
                            // Log the error and continue processing the next request
                        } processedRequests++;
                    }
                }
            });
        
    }
        public static bool IsValidUserID(string code)
        {
            // if (!code.StartsWith("lic_") && !code.StartsWith("inv_")) return false;
            string pattern = @"^usr_[a-f0-9]{8}-[a-f0-9]{4}-[a-f0-9]{4}-[a-f0-9]{4}-[a-f0-9]{12}$";
            return Regex.IsMatch(code, pattern, RegexOptions.IgnoreCase);
        }

    }
    internal struct Request
    {
        internal string userId;
        internal string userName;
        internal Action<bool, TagData> onComplete;
    }
    
    
}
