using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class ChargeTracker : MonoBehaviour
    {
        public FuelCellPuzzle fcp;
        public ChargeTracker other;

        public Image chargeImage;

        public Button transferButton;
        public Button chargeButton;
        public Button drainButton;

        public float chargeSpeed = 10f;

        public float cellMax;
        private float cellcurrent;


        public float CellCurrent
        {
            get { return cellcurrent; }
            set
            {
                cellcurrent = value;
                StartCoroutine(ChangeValue((float)(cellcurrent/cellMax)));
            }
        }


        private void Charge()
        {
            CellCurrent = cellMax;
        }


        private void Drain()
        {
            CellCurrent = 0;
        }


        private void Transfer()
        {
            float myCurrent = CellCurrent;
            float otherCurrent = other.CellCurrent;

            while (myCurrent > 0 && otherCurrent < other.cellMax)
            {
                myCurrent--;
                otherCurrent++;
            }

            CellCurrent = myCurrent;
            other.CellCurrent = otherCurrent;
        }


        private IEnumerator ChangeValue(float desiredAmount)
        {
            float elapsedTime = 0f;
            float originalAmount = chargeImage.fillAmount;
            while (elapsedTime < chargeSpeed)
            {
                chargeImage.fillAmount = Mathf.Lerp(originalAmount, desiredAmount, elapsedTime / chargeSpeed);

                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }
            fcp.CheckCompletion();
        }


        private void Awake()
        {
            chargeButton.onClick.AddListener(Charge);
            drainButton.onClick.AddListener(Drain);
            transferButton.onClick.AddListener(Transfer);
        }
    }
}

