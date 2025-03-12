using Assets.Code.ApiClient;
using Assets.Code.Models;
using Assets.Code.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
    public class Draggable : MonoBehaviour 
    {
        public Transform trans;
        public GameObject SideBar;

        public bool isDragging = false;
        public bool isOverTrashBin = false;

        public void StartDragging()
        {
            isDragging = true;
        }

        public void Update()
        {
            if (isDragging)
                trans.position = GetMousePosition();
        }

        private void OnMouseUpAsButton()
        {
            isDragging = !isDragging;

            if (!isDragging)
            {
                if (isOverTrashBin)
                {
                    DeleteObject deleteObject = FindAnyObjectByType<DeleteObject>();
                    if (deleteObject != null)
                    {
                        deleteObject.DeleteObject2D(gameObject);
                    }
                }
                else
                {
                    SideBar.SetActive(true);
                    SavePosition(gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("TrashBin"))
            {
                isOverTrashBin = true; 
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("TrashBin"))
            {
                isOverTrashBin = false; 
            }
        }

        private Vector3 GetMousePosition()
        {
            Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionInWorld.z = 0;
            return positionInWorld;
        }

        private void SavePosition(GameObject current)
        {
            CreateUpdateObject createObject = FindAnyObjectByType<CreateUpdateObject>();
            if (createObject != null)
            {
                createObject.SaveObject2D(current);
            }
        }
    }
}
