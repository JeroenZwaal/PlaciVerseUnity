using Assets.Code.ApiClient;
using Assets.Code.ApiClient.WebRequestResponses;
using Assets.Code.Environments;
using Assets.Code.Models;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Users
{
    public class UserLogin : MonoBehaviour
    {
        public User user;
        public UserApiClient userApiClient;
        public GetEnvironments getEnvironments;
        public TMP_InputField usernameInput;
        public TMP_InputField passwordInput;
        public TextMeshProUGUI errorText;
        public SceneLoader sceneLoader;

        public async void Login()
        {
            user.email = usernameInput.text;
            user.password = passwordInput.text;

            IWebRequestResponse webRequestResponse = await userApiClient.Login(user);

            switch (webRequestResponse)
            {
                case WebRequestData<string> dataResponse:
                    PlayerPrefs.SetString("Token", dataResponse.Data);
                    PlayerPrefs.Save();
                    sceneLoader.LoadEnvironmentsScene();
                    break;

                case WebRequestError errorResponse:
                    errorText.text = "Email or Password is Incorect";
                    break;

                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }
    }
}

