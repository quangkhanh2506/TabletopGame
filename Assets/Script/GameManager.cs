using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject FinishSide;
    [SerializeField] private GameObject startSide;
    [SerializeField] private List<GameObject> lsPosistions = new List<GameObject>();
    [SerializeField] private SetWay setWay;

    [HideInInspector] public GameObject NowMovePlayer;

    [HideInInspector] public int NowCountPosition;

    [HideInInspector] public float offsetPosition;

    [SerializeField] private UIGamePlay UIGamePlay;

    private List<GameObject> lsFinishPlayer = new List<GameObject>();

    [HideInInspector] public GameMode gameMode;

    private bool CanMouseDown;

    public RollDice rollDice;

    private bool isStart;

    private float CountDown;

    private PhotonView photonView;

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

        photonView = GetComponent<PhotonView>();
    }

    public void SetUp()
    {
        setWay.Onsetup();
        TurnManager.instance.SetUp();

        TurnManager.instance.player1.transform.position = new Vector3(startSide.transform.position.x + 0.03f, startSide.transform.position.y, startSide.transform.position.z);
        TurnManager.instance.player2.transform.position = new Vector3(startSide.transform.position.x - 0.03f, startSide.transform.position.y, startSide.transform.position.z);
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
                if (gameMode==GameMode.Offline ||(PhotonNetwork.IsMasterClient && TurnManager.instance.turn % 2 == 1) || (!PhotonNetwork.IsMasterClient && TurnManager.instance.turn % 2 == 0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.name == "Dice001")
                        {
                            CanMouseDown = false;
                            CountDown = 10;
                            StartCoroutine(ChangPosition());
                        }
                    }
                }

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
        int diceValue = rollDice.diceFaceNum;
        if (gameMode == GameMode.Online)
        {
            photonView.RPC("MovePlayer", RpcTarget.All, diceValue);
        }
        else
        {
            MovePlayer(diceValue);
        }
        
        
    }

    [PunRPC]
    public void MovePlayer(int countChangePosition)
    {
        StartCoroutine(Move(countChangePosition));
    }

    IEnumerator Move(int countChangePosition)
    {
        int i = 0;

        for (i = 0; i < countChangePosition && NowCountPosition < lsPosistions.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            NowMovePlayer.transform.position = new Vector3(lsPosistions[NowCountPosition].transform.position.x + offsetPosition, lsPosistions[NowCountPosition].transform.position.y, lsPosistions[NowCountPosition].transform.position.z);
            NowCountPosition++;
        }
        if (NowCountPosition == 23 && (countChangePosition - i) > 0)
        {
            yield return new WaitForSeconds(0.5f);
            NowCountPosition++;
            NowMovePlayer.transform.position = new Vector3(FinishSide.transform.position.x + offsetPosition, FinishSide.transform.position.y, FinishSide.transform.position.z);
            lsFinishPlayer.Add(NowMovePlayer);
        }

        if (setWay.BadWays.Contains(NowCountPosition - 1))
        {
            NowCountPosition -= 2;
            for (int j = 0; j < 3; j++)
            {
                yield return new WaitForSeconds(0.5f);
                NowMovePlayer.transform.position = new Vector3(lsPosistions[NowCountPosition].transform.position.x + offsetPosition, lsPosistions[NowCountPosition].transform.position.y, lsPosistions[NowCountPosition].transform.position.z);
                NowCountPosition--;
            }
            NowCountPosition += 2;
        }
        if (setWay.LuckyWays.Contains(NowCountPosition - 1))
        {
            yield return new WaitForSeconds(0.5f);
            NowMovePlayer.transform.position = new Vector3(lsPosistions[NowCountPosition].transform.position.x + offsetPosition, lsPosistions[NowCountPosition].transform.position.y, lsPosistions[NowCountPosition].transform.position.z);
            NowCountPosition++;
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

        if (gameMode == GameMode.Online)
        {
            if((PhotonNetwork.IsMasterClient && TurnManager.instance.turn % 2 == 1) || (!PhotonNetwork.IsMasterClient && TurnManager.instance.turn % 2 == 0))
            {
                CanMouseDown = TurnManager.instance.Result();
            }
        }
        else
        {
            CanMouseDown = TurnManager.instance.Result();
        }
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

public enum GameMode
{
    Offline,
    Online,
    Count
}