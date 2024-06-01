using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [SerializeField] private List<GameObject> lsUI = new List<GameObject>();
    [SerializeField] private GameObject BackGround;


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
    private void Start()
    {
        ShowUI(UI.UIMainMenu);
        ShowBG();
    }

    public void ShowUI(UI uI)
    {
        lsUI[(int)uI].SetActive(true);
    }

    public void HideUI(UI uI)
    {
        lsUI[(int)uI].SetActive(false);
    }

    public void ShowBG()
    {
        BackGround.SetActive(true);

    }

    public void HideBG()
    {
        BackGround.SetActive(false);
    }

}


public enum UI
{
    UIMainMenu,
    UIOption,
    UIGamePlay,
    UIResult,
    Count
}