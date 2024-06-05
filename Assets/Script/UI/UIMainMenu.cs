using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public void On_clickStartGame()
    {
        UIManager.instance.ShowUI(UI.UIGamePlay);
        GameManager.instance.gameMode = GameMode.Offline;
        GameManager.instance.SetUp();
        UIManager.instance.HideBG();
        UIManager.instance.HideUI(UI.UIMainMenu);
    }

    public void On_ClickFindPlayer()
    {
        //Access photon
        UIManager.instance.HideUI(UI.UIMainMenu);
        NetworkManager.instance.ConnectToPhoton();
        
    }
}
