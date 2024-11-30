using System;
using System.IO;
using System.Reflection;

namespace ZuxiTags
{
    internal class ResourceUtils
    {
        internal static void RegisterAssemResolver() {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, eventArgs) =>
            {
                if (eventArgs.Name.Contains("Newtonsoft.Json"))
                {
                    byte[] rawAssembly = ExtractResource("ZuxiTags.Newtonsoft.Json.dll");
                    return Assembly.Load(rawAssembly);
                }
                return null;
            };
        }
        internal static string[] EmbededLibraryPaths = new string[1] { "ZuxiTags.Newtonsoft.Json.dll" };
        internal static void ExtractResources()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            for (int i = 0; i < EmbededLibraryPaths.Length; i++)
            {
                string EmbededName = EmbededLibraryPaths[i];
                try
                {
                    byte[] rawAssembly = ExtractResource(EmbededName);

                    if (EmbededName.ToLower().Contains("json"))
                    {
                        try
                        {
                           Assembly NewtonSoft = Assembly.Load(rawAssembly);
                            LogManager.Log($"Loaded: {NewtonSoft.FullName}");
                            //File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), EmbededName.Replace("ZuxiTags.", "")), rawAssembly);
                        }
                        catch (Exception) { }
                        }

                }
                catch (Exception ex)
                {
                    LogManager.Log(ex.ToString());
                }
            }
        }

        internal static byte[] ExtractResource(string filename)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Stream stream = executingAssembly.GetManifestResourceStream(filename);
            if (stream == null)
            {
                return null;
            }
            byte[] array = new byte[stream.Length];
            stream.Read(array, 0, array.Length);
            return array;
        }
    }
}

