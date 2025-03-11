using Assets.Code;
using Assets.Code.ApiClient;
using Assets.Code.ApiClient.WebRequestResponses;
using Assets.Code.Models;
using Unity.VisualScripting;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public Object2DApiClient object2DApiClient;
    public GameObject sideBar;
    public GameObject carPrefab;
    public GameObject currentCar;
    private int prefabId;
 

    public void CreateBlueCar(Sprite newSpriteImage)
    {
        prefabId = 1;
        CreateCar(newSpriteImage, 180);
    }

    public void CreateRedCar(Sprite newSpriteImage)
    {
        prefabId = 2;
        CreateCar(newSpriteImage, 0);
    }

    public void CreateDarkBlueCar(Sprite newSpriteImage)
    {
        prefabId = 3;
        CreateCar(newSpriteImage, 180);
    }

    public void CreateCar(Sprite newSpriteImage, int rotationZ)
    {
        sideBar.SetActive(false);
        Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionInWorld.z = 0;

        GameObject clone = Instantiate(carPrefab, positionInWorld, Quaternion.identity);
        clone.GetComponent<Draggable>().SideBar = sideBar;
        clone.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        clone.transform.position = positionInWorld;

        SpriteRenderer spriteRenderer = clone.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = newSpriteImage;
        }

        Draggable logicScript = clone.GetComponent<Draggable>();
        if (logicScript != null)
        {
            logicScript.isDragging = true;
        }
        clone.GetComponent<Renderer>().sortingLayerName = "Prefab";
        currentCar = clone;
        
    }

    public void SaveObject2D()
    {
        if (currentCar == null) return;

        Object2D object2D = new Object2D
        {
            EnvironmentId = PlayerPrefs.GetInt("CurrentEnvironmentId"),
            PrefabId = prefabId,
            PositionX = currentCar.transform.position.x,
            PositionY = currentCar.transform.position.y,
            scaleX = currentCar.transform.localScale.x,
            scaleY = currentCar.transform.localScale.y,
            RotationZ = currentCar.transform.rotation.eulerAngles.z,
            SortingLayer = currentCar.GetComponent<Renderer>().sortingLayerID
        };

        CreateObject2D(object2D);
    }

    public async void CreateObject2D(Object2D object2D)
    {
        IWebRequestResponse response = await object2DApiClient.CreateObject2D(object2D);

        switch (response)
        {
            case WebRequestData<Object2D> data:
                //Debug.Log($"Aangemaakt met ID: {data.Data.id}");
                break;
            case WebRequestError error:
                //Debug.LogError($"Fout: {error.ErrorMessage}");
                break;
        }
    }
}
