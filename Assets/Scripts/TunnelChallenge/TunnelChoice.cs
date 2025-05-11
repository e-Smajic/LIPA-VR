using UnityEngine;

public class TunnelChoice : MonoBehaviour
{
    public bool isCorrect = false;
    public GameObject floor;
    public Minecart minecart;
    public TC_Main mainScript;
    private bool isFalling = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (!isCorrect)
            {
                /// Rigidbody rb = floor.GetComponent<Rigidbody>();
                /// rb.useGravity = true;
                /// rb.AddForce(Physics.gravity * (50f - 1), ForceMode.Acceleration);
                isFalling = true;
                minecart.downSpeed = minecart.forwardSpeed;
                minecart.forwardSpeed /= 2;
                minecart.sideSpeed = 0f;
            }
            else
            {
                mainScript.NextQuestion();
            }
            Debug.Log(isCorrect);
        }
    }

    void Update()
    {
        if (isFalling)
            floor.transform.Translate(- Vector3.forward.normalized * minecart.downSpeed * 1.2f * Time.deltaTime);
    }
}
