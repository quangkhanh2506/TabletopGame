using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    RollDice dice;

    private void Awake()
    {
        dice = FindObjectOfType<RollDice>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (dice != null)
        {
            if (dice.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                switch (int.Parse(other.name))
                {
                    case 1:
                        dice.diceFaceNum = 6;
                        break;
                    case 2:
                        dice.diceFaceNum = 4;
                        break;
                    case 3:
                        dice.diceFaceNum = 5;
                        break;
                    case 4:
                        dice.diceFaceNum = 2;
                        break;
                    case 5:
                        dice.diceFaceNum = 3;
                        break;
                    case 6:
                        dice.diceFaceNum = 1;
                        break;

                }
            }
        }
    }
}
