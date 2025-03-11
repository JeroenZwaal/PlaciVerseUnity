using Assets.Code.ApiClient.WebRequestResponses;
using Assets.Code.ApiClient;
using Assets.Code.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using Newtonsoft.Json;

namespace Assets.Code.ApiClient
{
    public class Environment2DApiClient : MonoBehaviour
    {
        public WebClient webClient;

        public async Task<IWebRequestResponse> ReadEnvironment2Ds()
        {
            string route = "/environments";
            string token = PlayerPrefs.GetString("Token", "");

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + token },
                { "Content-Type", "application/json" }
            };

            IWebRequestResponse webRequestResponse = await webClient.SendGetRequest(route, headers);
            return ParseEnvironment2DListResponse(webRequestResponse);
        }

        public async Awaitable<IWebRequestResponse> CreateEnvironment(Environment2D environment)
        {
            string route = "/environments";
            string data = JsonUtility.ToJson(environment);
            string token = PlayerPrefs.GetString("Token", "");

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + token },
                { "Content-Type", "application/json" }
            };

            IWebRequestResponse webRequestResponse = await webClient.SendPostRequest(route, data, headers);
            return ParseEnvironment2DResponse(webRequestResponse);
        }

        public async Awaitable<IWebRequestResponse> DeleteEnvironment(int environmentId)
        {
            string route = "/environments/" + environmentId;
            string token = PlayerPrefs.GetString("Token", "");

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + token },
                { "Content-Type", "application/json" }
            };

            return await webClient.SendDeleteRequest(route, headers: headers);
        }

        private IWebRequestResponse ParseEnvironment2DResponse(IWebRequestResponse webRequestResponse)
        {
            switch (webRequestResponse)
            {
                case WebRequestData<string> data:
                    Environment2D environment = JsonUtility.FromJson<Environment2D>(data.Data);
                    WebRequestData<Environment2D> parsedWebRequestData = new WebRequestData<Environment2D>(environment);
                    return parsedWebRequestData;
                default:
                    return webRequestResponse;
            }
        }

        private IWebRequestResponse ParseEnvironment2DListResponse(IWebRequestResponse webRequestResponse)
        {
            switch (webRequestResponse)
            {
                case WebRequestData<string> data:
                    //Debug.Log("Response data raw: " + data.Data);
                    List<Environment2D> environment2Ds = JsonConvert.DeserializeObject<List<Environment2D>>(data.Data);
                    WebRequestData<List<Environment2D>> parsedWebRequestData = new WebRequestData<List<Environment2D>>(environment2Ds);
                    return parsedWebRequestData;
                default:
                    return webRequestResponse;
            }
        }
    }
}


