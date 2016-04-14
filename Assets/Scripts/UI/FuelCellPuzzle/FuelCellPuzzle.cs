﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class FuelCellPuzzle : Menu
    {
        public Transform roverParent;
        public Transform cameraInPos;
        public Transform cameraOutPos;

        public ChargeTracker rightCharger;
        public ChargeTracker leftCharger;

        public ContainerObject fuelCellContainer;

        public Button exitButton;

        public int desiredCharge = 5;

        private bool isComplete = false;

        private float minScale = 0.001f;
        private float maxScale = 0.24f;

        private GameObject cellObject;


        public void Open(GameObject lfc, GameObject rfc, Transform camPos, ContainerObject co, GameObject cell)
        {
            cameraInPos = camPos;

            fuelCellContainer = co;
            rightCharger.chargeBar = rfc;
            leftCharger.chargeBar = lfc;

            cellObject = cell;
            
            StartCoroutine(OpenCoroutine());
        }
        

        public void CheckCompletion()
        {
            if(!isComplete && rightCharger.CellCurrent == desiredCharge)
            {
                isComplete = true;
                //UIManager.GetMenu<Inventory>().AddInventoryItem(reward, 1);
                fuelCellContainer.gameObject.SetActive(true);
                fuelCellContainer.ForceInteract();

                rightCharger.Drain();
                leftCharger.Drain();

                cellObject.SetActive(false);

                Destroy(GameObject.FindObjectOfType<DrillPuzzleInitializer>().gameObject);

                StartCoroutine(CloseCoroutine());
            }
        }


        private IEnumerator OpenCoroutine()
        {
            Camera controlledCamera = Camera.main;
            GameObject.FindObjectOfType<PlayerStats>().DisableMovement();

            controlledCamera.GetComponent<CameraCollision>().enabled = false;

            float desiredTime = 2f;
            float elapsedTime = 0f;

            roverParent = controlledCamera.transform.parent;
            cameraOutPos = controlledCamera.transform;

            controlledCamera.transform.SetParent(cameraInPos);

            Vector3 startPos = controlledCamera.transform.localPosition;
            Quaternion startRot = controlledCamera.transform.localRotation;

            while (elapsedTime < desiredTime)
            {
                controlledCamera.transform.localPosition = Vector3.Lerp(startPos, Vector3.zero, elapsedTime / desiredTime);
                controlledCamera.transform.localRotation = Quaternion.Lerp(startRot, Quaternion.identity, elapsedTime / desiredTime);

                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }

            controlledCamera.transform.localPosition = Vector3.zero;
            controlledCamera.transform.localRotation = Quaternion.identity;

            controlledCamera.transform.SetParent(roverParent);

            Open();
        }


        private IEnumerator CloseCoroutine()
        {
            rightCharger.Drain();
            leftCharger.Drain();

            yield return new WaitForSeconds(1f);
            Camera controlledCamera = Camera.main;

            controlledCamera.transform.localPosition = cameraOutPos.localPosition;
            controlledCamera.transform.localEulerAngles = Vector3.zero;

            Close();

            GameObject.FindObjectOfType<PlayerStats>().EnableMovement();
            controlledCamera.GetComponent<CameraCollision>().enabled = true;
        }


        private void StartClose()
        {
            StartCoroutine(CloseCoroutine());
        }


        private void Awake()
        {
            exitButton.onClick.AddListener(StartClose);
        }
    }
}

