using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTimer;

    public void ShowTimer(float timer)
    {
        txtTimer.text = ((int)timer).ToString();
    }

    public void On_ClickOption()
    {
        GameManager.instance.PauseGame();
        UIManager.instance.ShowUI(UI.UIOption);
        UIManager.instance.ShowBG();

    }

    
}
