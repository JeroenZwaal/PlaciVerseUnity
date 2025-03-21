using Assets.Code.ApiClient.WebRequestResponses;
using Assets.Code.ApiClient;
using Assets.Code.Models;
using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting.Antlr3.Runtime;

namespace Assets.Code.ApiClient
{
    public class Object2DApiClient : MonoBehaviour
    {
        public WebClient webClient;

        public async Awaitable<IWebRequestResponse> ReadObject2Ds(int environmentId)
        {
            string route = "/objects?envId=" + environmentId;
            string token = PlayerPrefs.GetString("Token", "");

            Dictionary<string, string> headers = new Dictionary<string, string>
    {
            { "Authorization", "Bearer " + token },
            { "Content-Type", "application/json" }
    };

            IWebRequestResponse webRequestResponse = await webClient.SendGetRequest(route, headers);
            return ParseObject2DListResponse(webRequestResponse);
        }

        public async Awaitable<IWebRequestResponse> CreateObject2D(Object2D object2D)
        {
            string route = "/objects?envId=" + object2D.EnvironmentId;
            string data = JsonUtility.ToJson(object2D);
            string token = PlayerPrefs.GetString("Token", "");

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + token },
                { "Content-Type", "application/json" }
            };

            IWebRequestResponse webRequestResponse = await webClient.SendPostRequest(route, data, headers);
            return ParseObject2DResponse(webRequestResponse);
        }

        public async Awaitable<IWebRequestResponse> UpdateObject2D(Object2D object2D)
        {
            string route = "/objects/" + object2D.ObjectId;
            string data = JsonUtility.ToJson(object2D);
            string token = PlayerPrefs.GetString("Token", "");

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + token },
                { "Content-Type", "application/json" }
            };

            return await webClient.SendPutRequest(route, data, headers);
        }

        public async Awaitable<IWebRequestResponse> DeleteObject2D(int ObjectId)
        {
            string route = "/objects/" + ObjectId;
            string token = PlayerPrefs.GetString("Token", "");

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + token },
                { "Content-Type", "application/json" }
            };

            return await webClient.SendDeleteRequest(route, headers: headers);
        }

        private IWebRequestResponse ParseObject2DResponse(IWebRequestResponse webRequestResponse)
        {
            switch (webRequestResponse)
            {
                case WebRequestData<string> data:
                    //Debug.Log("Response data raw: " + data.Data);
                    Object2D object2D = JsonConvert.DeserializeObject<Object2D>(data.Data);
                    WebRequestData<Object2D> parsedWebRequestData = new WebRequestData<Object2D>(object2D);
                    return parsedWebRequestData;
                default:
                    return webRequestResponse;
            }
        }

        private IWebRequestResponse ParseObject2DListResponse(IWebRequestResponse webRequestResponse)
        {
            switch (webRequestResponse)
            {
                case WebRequestData<string> data:
                    //Debug.Log("Response data raw: " + data.Data);
                    List<Object2D> environments = JsonConvert.DeserializeObject<List<Object2D>>(data.Data);
                    WebRequestData<List<Object2D>> parsedData = new WebRequestData<List<Object2D>>(environments);
                    return parsedData;
                default:
                    return webRequestResponse;
            }
        }
    }
}
