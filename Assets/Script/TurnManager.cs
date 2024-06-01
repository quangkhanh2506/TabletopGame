using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    public GameObject player1;
    public GameObject player2;

    [HideInInspector]public int turn;

    [HideInInspector]public int CountpositionPlayer1;
    [HideInInspector]public int CountpositionPlayer2;

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
    public void SetUp()
    {
        turn = 0;
        CountpositionPlayer1 = 0;
        CountpositionPlayer2 = 0;
    }


    public void ChangeTurn()
    {
        if(CountpositionPlayer1 <= 23 && CountpositionPlayer2 <= 23)
        {
            turn++;
        }

        if(CountpositionPlayer1 > 23)
        {
            turn = 2;
        }
        else if (CountpositionPlayer2 > 23)
        {
            turn = 1;
        }


        if (turn % 2 == 1 && CountpositionPlayer1 <= 23)
        {
            GameManager.instance.NowMovePlayer = player1;
            GameManager.instance.NowCountPosition = CountpositionPlayer1;
            GameManager.instance.offsetPosition = 0.03f;
        }
        else if(CountpositionPlayer2 <= 23)
        {
            GameManager.instance.NowMovePlayer = player2;
            GameManager.instance.NowCountPosition = CountpositionPlayer2;
            GameManager.instance.offsetPosition = - 0.03f;
        }
    }

    public bool Result()
    {
        if (GameManager.instance.LsRankPlayer().Count == 2)
        {
            GameManager.instance.EndGame();
            UIManager.instance.ShowUI(UI.UIResult);
            UIManager.instance.ShowBG();
            UIManager.instance.HideUI(UI.UIGamePlay);
            return false;
        }
        return true;
    }
}
