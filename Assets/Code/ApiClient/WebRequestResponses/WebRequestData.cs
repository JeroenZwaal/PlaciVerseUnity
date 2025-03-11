using UnityEngine;

namespace Assets.Code.ApiClient.WebRequestResponses
{
    public class WebRequestData<T> : IWebRequestResponse
    {
        public readonly T Data;

        public WebRequestData(T data)
        {
            Data = data;
        }
    }
}
