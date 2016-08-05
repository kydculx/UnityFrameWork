using UnityEngine;
using System.Collections;


public class PopUp : Pannel
{
    public delegate void PopUpEvent();  // void 타입 delegate 선억 
    public event PopUpEvent Yes;        // yes클릭시 실행할 event선언 
    public event PopUpEvent No;         // no클릭시 실행할 event선언
    public event PopUpEvent Ok;
    public event PopUpEvent Cancel;

    public void Yes() { Yes(); }
    public void No() { No(); }
    public void Ok() { Ok(); }
    public void Cancel() { Cancel(); }
}

public class FWPopupManager : FWSingleton<FWPopupManager>
{
}
