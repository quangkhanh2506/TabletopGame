using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void ConnectToPhoton()
    {

        if (!GameManager.instance.isLeaveRoom)
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
            }
            else
            {
                OnConnectedToMaster();
            }
        }
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        if (!GameManager.instance.isLeaveRoom)
        {
            JoinRandomRoom();
        }
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join random room. Creating a new room.");
        CreateRoom();
    }

    public void CreateRoom()
    {
        string roomName = "Room_" + Random.Range(1000, 9999); // Generate a random room name
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomName, options);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        CheckPlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered room: " + newPlayer.NickName);
        CheckPlayers();
    }

    private void CheckPlayers()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (photonView != null)
            {
                photonView.RPC("StartGame", RpcTarget.All);
            }
            else
            {
                Debug.LogError("photonView is null.");
            }
        }
    }


    [PunRPC]
    private void StartGame()
    {
        UIManager.instance.ShowUI(UI.UIGamePlay);
        GameManager.instance.gameMode = GameMode.Online;
        GameManager.instance.SetUp();
        UIManager.instance.HideBG();
    }
}
