using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class UIGamePlay : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI txtTimer;

    public void ShowTimer(float timer)
    {
        txtTimer.text = ((int)timer).ToString();
    }

    public void On_ClickOption()
    {
        if (GameManager.instance.gameMode == GameMode.Online)
        {
            GameManager.instance.photonView.RPC("PauseGame", RpcTarget.All);
        }
        else
        {
            GameManager.instance.PauseGame();
        }
        
        UIManager.instance.ShowUI(UI.UIOption);
        UIManager.instance.ShowBG();

    }


}
