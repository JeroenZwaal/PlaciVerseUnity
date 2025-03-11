using Assets.Code.ApiClient.WebRequestResponses;
using Assets.Code.ApiClient;
using Assets.Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Environments
{
    public class DeleteEnvironment : MonoBehaviour
    {
        public Environment2DApiClient enviroment2DApiClient;
        public GetEnvironments getEnvironments;
        public Environment2D environment2D;

        public async void DeleteEnvironment2D(int id)
        {
            IWebRequestResponse webRequestResponse = await enviroment2DApiClient.DeleteEnvironment(id);

            switch (webRequestResponse)
            {
                case WebRequestData<string> dataResponse:
                    string responseData = dataResponse.Data;
                    getEnvironments = FindObjectOfType<GetEnvironments>();
                    getEnvironments.GetEnvironment2Ds();
                    break;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    //Debug.Log("Delete environment error: " + errorMessage);
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }
    }
}
