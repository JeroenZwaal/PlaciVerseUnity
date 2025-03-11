using Assets.Code.Environments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code
{
    public class SceneLoader : MonoBehaviour
    {
        public GetEnvironments getEnvironments;
        //public GetObjects getObjects;

        public void LoadLoginScene()
        {
            SceneManager.LoadScene("LoginScene");
        }

        public void LoadRegisterScene()
        {
            SceneManager.LoadScene("RegisterScene");
        }

        public void LoadHomeScene()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public async void LoadEnvironmentsScene()
        {
            await SceneManager.LoadSceneAsync("AllEnvironmentsScene");
            await Task.Yield();
            getEnvironments = FindObjectOfType<GetEnvironments>();
            getEnvironments.GetEnvironment2Ds();
        }

        public async void LoadCreateEnvironmentScene()
        {
            await SceneManager.LoadSceneAsync("CreateEnvironmentScene");
            await Task.Yield();
        }

    }
}
