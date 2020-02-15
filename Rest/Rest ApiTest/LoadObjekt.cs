using System;
using System.IO;
using System.Text.Json;
namespace Rest_ApiTest
{
    public static class LoadObjekt
    {
        public static T Jason<T>(string FileURL) where T : class
        {
            T load= JsonSerializer.Deserialize<T>(File.ReadAllText(FileURL));
            return load;
        }
    }
}
