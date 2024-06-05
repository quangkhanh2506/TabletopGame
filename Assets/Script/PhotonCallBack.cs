using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonCallBack : MonoBehaviourPunCallbacks, IOnEventCallback
{
    private const byte PlayerLeftEventCode = 1;

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        object content = null;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(PlayerLeftEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == PlayerLeftEventCode)
        {
            GameManager.instance.ClearPlayer();
            UIManager.instance.ShowUI(UI.UIMainMenu);
            UIManager.instance.ShowBG();
            GameManager.instance.EndGame();
            GameManager.instance.isLeaveRoom = true;
            PhotonNetwork.LeaveRoom();
            UIManager.instance.HideUI(UI.UIGamePlay);
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    
}
