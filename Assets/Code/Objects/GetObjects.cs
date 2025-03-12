using Assets.Code;
using Assets.Code.ApiClient;
using Assets.Code.ApiClient.WebRequestResponses;
using Assets.Code.Models;
using Assets.Code.Objects;
using System.Collections.Generic;
using UnityEngine;

public class GetObjects : MonoBehaviour
{
    public Object2DApiClient object2DApiClient;
    public GameObject sideBar;
    public GameObject prefab;

    public void Start()
    {
        int id = PlayerPrefs.GetInt("CurrentEnvironmentId");
        GetObjects2D(id);
    }

    public async void GetObjects2D(int id)
    {
        IWebRequestResponse webRequestResponse = await object2DApiClient.ReadObject2Ds(id);

        switch (webRequestResponse)
        {
            case WebRequestData<List<Object2D>> dataResponse:
                List<Object2D> object2Ds = dataResponse.Data;

                foreach (Object2D obj in object2Ds)
                {
                    LoadSprites(obj);
                }
                break;
           
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                break;
            default:
                throw new System.NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }

    }

    private void LoadSprites(Object2D obj)
    {
        GameObject newObject = Instantiate(prefab, new Vector3(obj.PositionX, obj.PositionY, 0), Quaternion.identity);
        newObject.transform.rotation = Quaternion.Euler(0, 0, obj.RotationZ);
        newObject.GetComponent<Draggable>().SideBar = sideBar;

        ObjectIdentifier identifier = newObject.GetComponent<ObjectIdentifier>();
        if (identifier == null)
        {
            identifier = newObject.AddComponent<ObjectIdentifier>();
        }
        identifier.ObjectId = obj.ObjectId;
        identifier.PrefabId = obj.PrefabId;

        SpriteRenderer spriteRenderer = newObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            switch (obj.PrefabId)
            {
                case 1:
                    spriteRenderer.sprite = Resources.Load<Sprite>("BlueCar");
                    break;
                case 2:
                    spriteRenderer.sprite = Resources.Load<Sprite>("RedCar");
                    break;
                case 3:
                    spriteRenderer.sprite = Resources.Load<Sprite>("DarkBlueCar");
                    break;
            }

        }

        Draggable logicScript = newObject.GetComponent<Draggable>();
        if (logicScript != null)
        {
            logicScript.isDragging = false;
        }
        newObject.GetComponent<Renderer>().sortingLayerName = "Prefab";

    }
}
