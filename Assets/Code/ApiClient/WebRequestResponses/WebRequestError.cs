using Assets.Code.Models;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

namespace Assets.Code.ApiClient.WebRequestResponses
{
    public class WebRequestError : IWebRequestResponse
    {
        public string ErrorMessage;
        public WebRequestError(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                ErrorMessage = "No errors";
                return;
            }

            if (!errorMessage.StartsWith("{"))
            {
                ErrorMessage = errorMessage;
                return;
            }

            ApiResponse response = JsonConvert.DeserializeObject<ApiResponse>(errorMessage);

            if (response.errors != null)
            {
                ErrorMessage = response.errors.Values.First().First();
            }
            else
            {
                ErrorMessage = response.title;
            }
        }
    }
}
