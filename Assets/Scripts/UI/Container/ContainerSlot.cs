using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Sol
{
    public class ContainerSlot : InventorySlot
    {
        private void OpenTransferToolTip(bool b)
        {
            //Debug.Log("opening tooltip from container");

           // TransferToolTip toolTip = UIManager.Open<TransferToolTip>();
           // toolTip.SetContent(ii, true);
        }


        private void Awake()
        {
            moreInfo.onValueChanged.AddListener(OpenTransferToolTip);
        }
    }

}
