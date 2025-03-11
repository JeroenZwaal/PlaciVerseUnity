using Assets.Code.ApiClient;
using Assets.Code.ApiClient.WebRequestResponses;
using Assets.Code.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Assets.Code.Users
{
    public class UserRegister : MonoBehaviour
    {
        public User user;
        public UserApiClient userApiClient;
        public TMP_InputField emailInput;
        public TMP_InputField passwordInput;
        public TextMeshProUGUI errorText;
        public SceneLoader sceneLoader;

        public async void Register()
        {
            user.email = emailInput.text;
            user.password = passwordInput.text;

            IWebRequestResponse webRequestResponse = await userApiClient.Register(user);

            switch (webRequestResponse)
            {
                case WebRequestData<string> dataResponse:
                    sceneLoader.LoadLoginScene();
                    break;
                case WebRequestError errorResponse:
                    errorText.text = errorResponse.ErrorMessage;
                    break;

                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }
    }
}
