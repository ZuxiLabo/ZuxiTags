using System;
using System.Diagnostics;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace ZuxiTags
{
    internal class Server
    {
       
        WebClient webClient; 
       

        public string getUserByName(string name)
        {
            string aabc = webClient.DownloadString("getuserbyname?name=" + name);
            Debug.WriteLine(aabc);
            return aabc;
        }
         
        public VRCPlayer GetVRCPlayerbyName(string name)
        {
            Debug.WriteLine(name);
            return PlayerManager.GetUserFromJSON(getUserByName(HttpUtility.UrlEncode(name)), name);
        }



        public string GetUserTagByID(string id)
        {
            Debug.WriteLine(id);
            return webClient.DownloadString("fetchtaglist?uid=" + id);
        }
        public Server()
        {
            webClient = new WebClient();
            webClient.BaseAddress = "https://api.uwu.ac/api/v7/vrchat/";
            webClient.Headers.Add(HttpRequestHeader.UserAgent, "ZuxiLabo.UAPublic.ce5e4893f53c6e2bf3e9ffc21d23a6f6fe03d866b867e257c1b1346d1acfb59e");

           
        }
    }
}
