using Assets.Code.ApiClient.WebRequestResponses;
using Assets.Code.ApiClient;
using Assets.Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Code.Environments
{
    public class CreateEnvironment : MonoBehaviour
    {
        public Environment2DApiClient enviroment2DApiClient;
        public GetEnvironments getEnvironments;
        public Environment2D environment2D;
        public TMP_InputField nameInput;
        public TMP_InputField heightInput;
        public TMP_InputField lenghtInput;
        public TextMeshProUGUI errorText;

        public async void CreateEnvironment2D()
        {
            int height = int.Parse(heightInput.text);
            int length = int.Parse(lenghtInput.text);
            string newName = nameInput.text.Trim();
            environment2D.Name = newName;
            environment2D.MaxLenght = length;
            environment2D.MaxHeight = height;

            IWebRequestResponse createResponse = await enviroment2DApiClient.CreateEnvironment(environment2D);

            switch (createResponse)
            {
                case WebRequestData<Environment2D> dataResponse:
                    environment2D.Id = dataResponse.Data.Id;
                    await SceneManager.LoadSceneAsync("AllEnvironmentsScene");
                    await Task.Yield();
                    getEnvironments = FindObjectOfType<GetEnvironments>();
                    getEnvironments.GetEnvironment2Ds();
                    break;
                case WebRequestError errorResponse:
                    errorText.text = errorResponse.ErrorMessage;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

