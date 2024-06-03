using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWay : MonoBehaviour
{
    [SerializeField] private List<GameObject> lsWays = new List<GameObject>();
    public List<int> BadWays = new List<int> { 2, 8, 16, 22 };
    public List<int> LuckyWays = new List<int> { 3, 10, 14, 18 };

    [SerializeField] private Material Lucky;
    [SerializeField] private Material Badlucky;

    public void Onsetup()
    {
        for (int i = 0; i < BadWays.Count; i++)
        {
            lsWays[BadWays[i]].GetComponent<Renderer>().material.color = Badlucky.color;
            if (lsWays[BadWays[i]].transform.childCount > 1)
            {
                lsWays[BadWays[i]].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Badlucky.color;
            }
        }

        for (int i = 0; i < LuckyWays.Count; i++)
        {
            lsWays[LuckyWays[i]].GetComponent<Renderer>().material.color = Lucky.color;
            if (lsWays[LuckyWays[i]].transform.childCount > 1)
            {
                lsWays[LuckyWays[i]].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Lucky.color;
            }
        }
    }
}
