using UnityEngine;
using System.Collections;


public class FWPaneal : MonoBehaviour
{

}


public class FWPopup : FWPaneal
{
    public delegate void PopUpEvent();
    public event PopUpEvent Yes;
    public event PopUpEvent No;
    public event PopUpEvent Ok;
    public event PopUpEvent Cancel;

    public void ClickYes() { Yes(); }
    public void ClickNo() { No(); }
    public void ClickOk() { Ok(); }
    public void ClickCancel() { Cancel(); }
}

public class FWPopupManager : FWSingleton<FWPopupManager>
{
}
