using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class UIOption : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject PositionResetPlayer1;
    [SerializeField] GameObject PositionResetPlayer2;

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
        UIManager.instance.HideUI(UI.UIGamePlay);
        if (GameManager.instance.gameMode == GameMode.Online)
        {
            GameManager.instance.isLeaveRoom = true;
            PhotonNetwork.LeaveRoom();
        }

        TurnManager.instance.player1.transform.position = new Vector3(PositionResetPlayer1.transform.position.x, PositionResetPlayer1.transform.position.y, PositionResetPlayer1.transform.position.z);
        TurnManager.instance.player2.transform.position = new Vector3(PositionResetPlayer2.transform.position.x, PositionResetPlayer2.transform.position.y, PositionResetPlayer2.transform.position.z);
        GameManager.instance.gameMode = GameMode.Count;
        UIManager.instance.HideUI(UI.UIOption);
        
    }

    
}
