using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class FuelCellPuzzle : Menu
    {
        public Transform cameraInPos;
        public Transform cameraOutPos;

        public Ingredient reward;

        public ChargeTracker rightCharger;
        public ChargeTracker leftCharger;

        public Button exitButton;

        public int desiredCharge = 5;

        private bool isComplete = false;

        private float minScale = 0.001f;
        private float maxScale = 0.24f;


        public void Open(GameObject lfc, GameObject rfc, Transform camPos)
        {
            cameraInPos = camPos;

            rightCharger.chargeBar = rfc;
            leftCharger.chargeBar = lfc;

            StartCoroutine(OpenCoroutine());
        }
        

        public void CheckCompletion()
        {
            if(!isComplete && rightCharger.CellCurrent == desiredCharge)
            {
                isComplete = true;
                UIManager.GetMenu<Inventory>().AddInventoryItem(reward, 1);

                StartCoroutine(CloseCoroutine());

                Destroy(GameObject.FindObjectOfType<DrillPuzzleInitializer>().gameObject);
            }
        }


        private IEnumerator OpenCoroutine()
        {
            Camera.main.GetComponent<CameraCollision>().enabled = false;

            float desiredTime = 2f;
            float elapsedTime = 0f;

            cameraOutPos = Camera.main.transform;

            while (elapsedTime < desiredTime)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraInPos.position, elapsedTime / desiredTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cameraInPos.rotation, elapsedTime / desiredTime);

                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }

            Camera.main.transform.position = cameraInPos.position;
            Open();
        }


        private IEnumerator CloseCoroutine()
        {
            float desiredTime = 2f;
            float elapsedTime = 0f;

            while (elapsedTime < desiredTime)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraOutPos.position, elapsedTime / desiredTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cameraOutPos.rotation, elapsedTime / desiredTime);

                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }

            Camera.main.transform.position = cameraOutPos.position;

            Close();
            Camera.main.GetComponent<CameraCollision>().enabled = true;
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

