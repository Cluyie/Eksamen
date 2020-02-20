using System;
using System.IO;
using System.Text.Json;
namespace Rest_ApiTest
{
    public static class LoadObjekt
    {
        public static T Json<T>(string FileURL) where T : class
        {
            if(PathTest(FileURL))
            {   
                throw new FileLoadException();
            }
            T load= JsonSerializer.Deserialize<T>(File.ReadAllText(FileURL));
            return load;
        }
        public static bool PathTest(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                return false;
            }
            return true;
        }
    }
}
