using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOption : MonoBehaviour
{
    public void On_ClickResume()
    {
        GameManager.instance.ResumeGame();
        UIManager.instance.HideBG();
        UIManager.instance.HideUI(UI.UIOption);
    }

    public void On_clickHome()
    {
        GameManager.instance.EndGame();
        UIManager.instance.ShowUI(UI.UIMainMenu);
        UIManager.instance.HideUI(UI.UIOption);
    }
}
