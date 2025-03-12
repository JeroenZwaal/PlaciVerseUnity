using Assets.Code.ApiClient;
using Assets.Code.ApiClient.WebRequestResponses;
using Assets.Code.Environments;
using Assets.Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Objects
{
    public class DeleteObject : MonoBehaviour
    {
        public Object2DApiClient object2DApiClient;

        public async void DeleteObject2D(GameObject gameObject)
        {
            ObjectIdentifier identifier = gameObject.GetComponent<ObjectIdentifier>();
            if (identifier == null)
            {
                Debug.LogError("ObjectIdentifier not found on object: " + gameObject.name);
                return;
            }

            IWebRequestResponse webRequestResponse = await object2DApiClient.DeleteObject2D(identifier.ObjectId) ;

            switch (webRequestResponse)
            {
                case WebRequestData<string> dataResponse:
                    string responseData = dataResponse.Data;
                    Destroy(gameObject);
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
