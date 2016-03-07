using UnityEngine;
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


        public void Open(GameObject lfc, GameObject rfc, Transform camPos, ContainerObject co)
        {
            cameraInPos = camPos;

            fuelCellContainer = co;
            rightCharger.chargeBar = rfc;
            leftCharger.chargeBar = lfc;

            StartCoroutine(OpenCoroutine());
        }
        

        public void CheckCompletion()
        {
            if(!isComplete && rightCharger.CellCurrent == desiredCharge)
            {
                isComplete = true;
                //UIManager.GetMenu<Inventory>().AddInventoryItem(reward, 1);
                fuelCellContainer.ForceInteract();

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

            while (elapsedTime < desiredTime)
            {
                controlledCamera.transform.localPosition = Vector3.Lerp(controlledCamera.transform.localPosition, Vector3.zero, elapsedTime / desiredTime);
                controlledCamera.transform.localRotation = Quaternion.Lerp(controlledCamera.transform.localRotation, Quaternion.identity, elapsedTime / desiredTime);

                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }

            controlledCamera.transform.localPosition = Vector3.zero;
            controlledCamera.transform.localRotation = Quaternion.identity;

            Open();
        }


        private IEnumerator CloseCoroutine()
        {
            rightCharger.Drain();
            leftCharger.Drain();

            yield return null;
            Camera controlledCamera = Camera.main;

            controlledCamera.transform.SetParent(roverParent);

            controlledCamera.transform.localPosition = cameraOutPos.localPosition;
            controlledCamera.transform.localEulerAngles = Vector3.zero;

            Close();

            GameObject.FindObjectOfType<PlayerStats>().EnableMovement();
            controlledCamera.GetComponent<CameraCollision>().enabled = true;
            Destroy(GameObject.FindObjectOfType<DrillPuzzleInitializer>().gameObject);
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

