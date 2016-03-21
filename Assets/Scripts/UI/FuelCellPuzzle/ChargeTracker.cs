using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class ChargeTracker : MonoBehaviour
    {
        public FuelCellPuzzle fcp;
        public ChargeTracker other;

        public GameObject chargeBar;

        public Button transferButton;
        public Button chargeButton;
        public Button drainButton;

        public float chargeSpeed = 10f;

        public float cellMax;
        private float cellScaleMax = 0.24f;
        private float cellcurrent;


        public float CellCurrent
        {
            get { return cellcurrent; }
            set
            {
                cellcurrent = value;
                StopAllCoroutines();
                StartCoroutine(ChangeValue((float)((cellcurrent / cellMax) *cellScaleMax )));
            }
        }


        public void Charge()
        {
            CellCurrent = cellMax;
        }


        public void Drain()
        {
            CellCurrent = 0;
        }


        public void Transfer()
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
            float originalAmount = chargeBar.transform.localScale.y;
            while (elapsedTime < chargeSpeed)
            {
                chargeBar.transform.localScale = new Vector3(
                    chargeBar.transform.localScale.x,
                    Mathf.Lerp(originalAmount, desiredAmount, elapsedTime / chargeSpeed),
                    chargeBar.transform.localScale.z);

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

