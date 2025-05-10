using UnityEngine;

public class Minecart : MonoBehaviour
{
    // Parameters:
    private bool isMoving = true;
    private float forwardSpeed = 5f;
    private float sideSpeed = 5f;
    private float sideBarriers = 20f;
    private Vector3 forwardDirection = Vector3.forward;
    private Vector3 sideDirection = Vector3.right;
    private Vector3 axis = -Vector3.up;

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
        world.transform.Translate(forwardDirection.normalized * forwardSpeed * Time.deltaTime);
        world.transform.Translate(sideDirection.normalized * sideSpeed * Time.deltaTime * (lever.angle / 30));
        Debug.Log(world.transform.position.x);
    }
}
