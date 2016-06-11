using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Sol
{
    public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static GameObject itemBeingDragged;

        private Vector3 startPosition;
        private Transform startParent;

        //[HideInInspector]
        public float dragLength = 0;

        #region IBeginDragHandler implementation
        public void OnBeginDrag(PointerEventData eventData)
        {
            itemBeingDragged = gameObject;
            startPosition = transform.position;
            startParent = transform.parent;
            dragLength = 0f;

            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        #endregion

        #region IDragHandler implementation
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 targetPosition = new Vector3(Input.mousePosition.x - 512f, Input.mousePosition.y - 384f, 0f);
            dragLength += Time.deltaTime;
            transform.position = targetPosition;
        }
        #endregion

        #region IEndDragHandler implementation
        public void OnEndDrag(PointerEventData eventData)
        {
            itemBeingDragged = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (transform.parent != startParent && dragLength >= 0.1f)
            {

            }
            else
            {
                transform.position = startPosition;
            }
        }
        #endregion
    }
}