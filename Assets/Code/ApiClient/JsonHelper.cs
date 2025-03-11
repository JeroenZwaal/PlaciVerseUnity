using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.ApiClient
{
    public static class JsonHelper
    {
        public static List<T> ParseJsonArray<T>(string jsonArray)
        {
            string extendedJson = "{\"list\":" + jsonArray + "}";
            JsonList<T> parsedList = JsonUtility.FromJson<JsonList<T>>(extendedJson);
            return parsedList.list;
        }

        public static string ExtractToken(string data)
        {
            Token token = JsonUtility.FromJson<Token>(data);
            return token.accessToken;
        }
    }

    [Serializable]
    public class JsonList<T>
    {
        public List<T> list;
    }
}
