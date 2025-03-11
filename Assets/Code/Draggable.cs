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
                SideBar.SetActive(true);
                SavePosition();
            }
        }

        private Vector3 GetMousePosition()
        {
            Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionInWorld.z = 0;
            return positionInWorld;
        }

        private void SavePosition()
        {
            CreateObject createObject = FindAnyObjectByType<CreateObject>();
            if (createObject != null)
            {
                createObject.SaveObject2D();
            }
        }
    }
}
