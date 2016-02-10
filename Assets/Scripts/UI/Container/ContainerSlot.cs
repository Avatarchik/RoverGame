using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ContainerSlot : InventorySlot
{
    private void OpenTransferToolTip()
    {
        Debug.Log("opening tooltip from container");

        TransferToolTip toolTip = UIManager.Open<TransferToolTip>();
        toolTip.SetContent(ii, true);
    }


    private void Awake()
    {
        moreInfo.onClick.AddListener(OpenTransferToolTip);
    }
}
