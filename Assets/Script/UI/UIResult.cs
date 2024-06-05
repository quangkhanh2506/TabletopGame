using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class UIResult : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> lsTxtRank = new List<TextMeshProUGUI>();
    [SerializeField] GameObject PositionResetPlayer1;
    [SerializeField] GameObject PositionResetPlayer2;
    private List<GameObject> lsRankofPlayer;

    private void Start()
    {
        if (GameManager.instance.LsRankPlayer().Count >= 2)
        {
            lsRankofPlayer = GameManager.instance.LsRankPlayer();
            for (int i = 0; i < lsTxtRank.Count; i++)
            {
                lsTxtRank[i].text = lsRankofPlayer[i].gameObject.name;
            }
        }
    }

    public void On_ClickHome()
    {
        GameManager.instance.ClearPlayer();
        UIManager.instance.ShowUI(UI.UIMainMenu);
        UIManager.instance.HideUI(UI.UIResult);
        if (GameManager.instance.gameMode == GameMode.Online)
        {
            PhotonNetwork.LeaveRoom();
        }

        TurnManager.instance.player1.transform.position = new Vector3(PositionResetPlayer1.transform.position.x, PositionResetPlayer1.transform.position.y, PositionResetPlayer1.transform.position.z);
        TurnManager.instance.player2.transform.position = new Vector3(PositionResetPlayer2.transform.position.x, PositionResetPlayer2.transform.position.y, PositionResetPlayer2.transform.position.z);
    }
}
