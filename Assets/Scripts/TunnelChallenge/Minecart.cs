using UnityEngine;

public class Minecart : MonoBehaviour
{
    // Parameters:
    public bool isMoving = false;
    public float forwardSpeed = 10f;
    public float sideSpeed = 10f;
    public float downSpeed = 0f;
    private float sideBarriers = 20f;
    private Vector3 forwardDirection = Vector3.forward;
    private Vector3 sideDirection = Vector3.right;
    private Vector3 axis = Vector3.down;

    // Environment:
    [Header("World")]
    public GameObject world;

    // Components:
    [Header("Minecart components")]
    public HingeJoint lever;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            world.transform.Translate(forwardDirection.normalized * forwardSpeed * Time.deltaTime);
            world.transform.Translate(sideDirection.normalized * sideSpeed * Time.deltaTime * (lever.angle / 30));
            world.transform.Translate(-axis.normalized * downSpeed * Time.deltaTime);
        }
        //Debug.Log(world.transform.position.x);
    }
}
