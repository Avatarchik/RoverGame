using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ContainerSlot : InventorySlot
{
    private void OpenTransferToolTip()
    {
        TransferToolTip toolTip = UIManager.Open<TransferToolTip>();
        toolTip.Open(ii, true);
    }


    private void Awake()
    {
        moreInfo.onClick.AddListener(OpenTransferToolTip);
    }
}
