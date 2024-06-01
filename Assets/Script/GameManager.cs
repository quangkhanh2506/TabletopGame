using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject FinishSide;
    [SerializeField] private GameObject startSide;
    [SerializeField] private List<GameObject> lsPosistions = new List<GameObject>();

    [HideInInspector] public GameObject NowMovePlayer;

    [HideInInspector] public int NowCountPosition;

    [HideInInspector] public float offsetPosition;

    [SerializeField] private UIGamePlay UIGamePlay;

    private List<GameObject> lsFinishPlayer = new List<GameObject>();

    private bool CanMouseDown;

    public RollDice rollDice;

    private bool isStart;

    private float CountDown;

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
        TurnManager.instance.SetUp();

        TurnManager.instance.player1.transform.position = new Vector3(startSide.transform.position.x+0.03f, startSide.transform.position.y, startSide.transform.position.z);

        TurnManager.instance.player2.transform.position = new Vector3(startSide.transform.position.x-0.03f, startSide.transform.position.y, startSide.transform.position.z);


        TurnManager.instance.ChangeTurn();

        CanMouseDown = true;

        isStart = true;
        CountDown = 10;
        UIGamePlay.ShowTimer(CountDown);

    }

    private void Update()
    {
        if (rollDice.GetComponent<Rigidbody>() != null && isStart)
        {
            if (Input.GetMouseButtonDown(0) && CanMouseDown)
            {
                CanMouseDown = false;
                CountDown = 10;
                StartCoroutine(ChangPosition());
            }
            else if (CanMouseDown)
            {
                CountDown -= Time.deltaTime;
                if (CountDown < 0)
                {
                    CanMouseDown = false;
                    CountDown = 10;
                    StartCoroutine(ChangPosition());
                    
                }
                // Show CountDown in UIGAMEPLAY
                UIGamePlay.ShowTimer(CountDown);
            }
            
        }
    }

    IEnumerator ChangPosition()
    {
        rollDice.DiceRoll();
        yield return new WaitForSeconds(2.25f);
        Debug.Log(rollDice.diceFaceNum);
        int countChangePosition = rollDice.diceFaceNum;

        int i = 0;

        for (i = 0; i < countChangePosition && NowCountPosition < lsPosistions.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            NowMovePlayer.transform.position = new Vector3(lsPosistions[NowCountPosition].transform.position.x + offsetPosition, lsPosistions[NowCountPosition].transform.position.y, lsPosistions[NowCountPosition].transform.position.z);
            NowCountPosition++;
        }
        if(NowCountPosition == 23 && (countChangePosition - i) > 0)
        {
            yield return new WaitForSeconds(0.5f);
            NowCountPosition++;
            NowMovePlayer.transform.position = new Vector3(FinishSide.transform.position.x + offsetPosition, FinishSide.transform.position.y, FinishSide.transform.position.z);
            lsFinishPlayer.Add(NowMovePlayer);
        }

        if (TurnManager.instance.turn % 2 == 0)
        {
            TurnManager.instance.CountpositionPlayer2 = NowCountPosition;
        }
        else
        {
            TurnManager.instance.CountpositionPlayer1 = NowCountPosition;
        }
        NowMovePlayer = null;
        TurnManager.instance.ChangeTurn();
        
        CanMouseDown = TurnManager.instance.Result();
    }

    public List<GameObject> LsRankPlayer()
    {
        return lsFinishPlayer;
    }

    public void ResumeGame()
    {
        isStart = true;
    }

    public void PauseGame()
    {
        isStart = false;
    }

    public void ClearPlayer()
    {
        lsFinishPlayer.Clear();
    }

    public void EndGame()
    {
        TurnManager.instance.player1.transform.position = new Vector3(startSide.transform.position.x + 0.03f, startSide.transform.position.y, startSide.transform.position.z);

        TurnManager.instance.player2.transform.position = new Vector3(startSide.transform.position.x - 0.03f, startSide.transform.position.y, startSide.transform.position.z);

        isStart = false;
        UIGamePlay.ShowTimer(10);
    }
}