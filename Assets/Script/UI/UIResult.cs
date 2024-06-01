using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIResult : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> lsTxtRank = new List<TextMeshProUGUI>();
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
    }
}
