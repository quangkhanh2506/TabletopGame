using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RollDice : MonoBehaviour
{
    Rigidbody body;

    [SerializeField] private float maxRandomForceValue, startRollingForce;

    private float forceX, forceY, forceZ;
    public int diceFaceNum;

    private void Awake()
    {
        Initialize();
    }


    public void DiceRoll()
    {
        body.isKinematic = false;

        forceX = Random.Range(0, maxRandomForceValue);
        forceY = Random.Range(0, maxRandomForceValue);
        forceZ = Random.Range(0, maxRandomForceValue);

        body.AddForce(Vector3.up * startRollingForce);
        body.AddTorque(forceX*3, forceY*3, forceZ*3);
    }

    private void Initialize()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);
    }
}
