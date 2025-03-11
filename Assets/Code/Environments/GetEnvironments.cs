using Assets.Code.ApiClient.WebRequestResponses;
using Assets.Code.ApiClient;
using Assets.Code.Models;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Environments
{
    public class GetEnvironments : MonoBehaviour
    {
        [SerializeField] public Transform contentParent;
        [SerializeField] public GameObject environmentPrefab;
        public Environment2DApiClient enviroment2DApiClient;

        //public GetObjects getObjects;
        public Environment2D environment2D;
        public DeleteEnvironment deleteEnvironment;

        public void Start()
        {
            GetEnvironment2Ds();
        }

        public async void GetEnvironment2Ds()
        {
            IWebRequestResponse webRequestResponse = await enviroment2DApiClient.ReadEnvironment2Ds();

            switch (webRequestResponse)
            {
                case WebRequestData<List<Environment2D>> dataResponse:
                    ClearList();
                    FillList(dataResponse.Data);
                    break;
                case WebRequestError errorResponse:
                    Debug.LogError($"Error: {errorResponse.ErrorMessage}");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void FillList(List<Environment2D> environments)
        {
            foreach (var environment in environments)
            {
                GameObject newEnv = Instantiate(environmentPrefab, contentParent);
                SetupEnvironmentUI(newEnv, environment, deleteEnvironment);
            }
        }

        void ConfigureButton(Transform parent, string buttonName, Action action)
        {
            Transform buttonTransform = parent.Find(buttonName);
            if (buttonTransform == null) return;

            Button button = buttonTransform.GetComponent<Button>();
            if (button == null) return;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => action?.Invoke());
        }

        void SetupEnvironmentUI(GameObject newEnv, Environment2D environment, DeleteEnvironment deleteEnvironment)
        {
            // 1. Naam instellen
            Transform nameButton = newEnv.transform.Find("NameButton");
            if (nameButton != null)
            {
                TextMeshProUGUI nameText = nameButton.GetComponentInChildren<TextMeshProUGUI>(true);
                if (nameText != null)
                    nameText.text = environment.Name;
            }

            // 2. Klikfunctionaliteit op knoppen instellen
            ConfigureButton(newEnv.transform, "NameButton", () => SeeEnvironment(environment.Id));
            ConfigureButton(newEnv.transform, "DeleteButton", () => deleteEnvironment.DeleteEnvironment2D(environment.Id));
        }

        public async void SeeEnvironment(int id)
        {
            PlayerPrefs.SetInt("CurrentEnvironmentId", id);
            PlayerPrefs.Save();
            await SceneManager.LoadSceneAsync("EnvironmentScene");
            //await Task.Yield();
            //getObjects = FindObjectOfType<GetObjects>();
            //getObjects.ReadObject2Ds(id);
        }

        public void ClearList()
        {
            for (int i = contentParent.childCount - 1; i >= 0; i--)
            {
                Destroy(contentParent.GetChild(i).gameObject);
            }
        }
    }
}
