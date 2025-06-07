using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class MovementScript : MonoBehaviour
{
    public GameObject standardMove;
    public GameObject teleportMove;
    public GameObject leftTeleportInteractor;
    public GameObject rightTeleportInteractor;
    public GameObject teleportArea;
    public GameObject turnMove;
    public GameObject turnController;
    public GameObject vignette;

    private enum MoveType {STANDARD, TELEPORTATION};
    private enum TurnType {SNAP, CONTINUOUS};
    private enum Tunneling {OFF, ON};
    private int[] turnAngles = {15, 30, 45};
    private int[] turnSpeeds = {30, 60, 90, 120};

    void LoadMovementType()
    {
        MoveType moveType = (MoveType) PlayerPrefs.GetInt("MovementType", 0);

        if (moveType == MoveType.STANDARD)
        {
            standardMove.SetActive(true);
            teleportArea.GetComponent<TeleportationArea>().enabled = false;
        }
        else
        {
            standardMove.SetActive(false);
            teleportArea.GetComponent<TeleportationArea>().enabled = true;
        }
    }

    void LoadMovementSpeed()
    {
        float speed = PlayerPrefs.GetFloat("MovementSpeed", 2.5f);
        standardMove.GetComponent<DynamicMoveProvider>().moveSpeed = speed;
    }

    void LoadTurnType()
    {
        TurnType turnType = (TurnType) PlayerPrefs.GetInt("TurnType", 0);

        if (turnType == TurnType.CONTINUOUS)
        {
            turnController.GetComponent<ControllerInputActionManager>().smoothTurnEnabled = true;
        }
    }

    void LoadTurnAngle()
    {
        int angle = turnAngles[PlayerPrefs.GetInt("TurnAngle", 2)];
        turnMove.GetComponent<SnapTurnProvider>().turnAmount = angle;
    }

    void LoadTurnSpeed()
    {
        int speed = turnSpeeds[PlayerPrefs.GetInt("TurnSpeed", 1)];
        turnMove.GetComponent<ContinuousTurnProvider>().turnSpeed = speed;
    }

    void LoadTunneling()
    {
        Tunneling tunneling = (Tunneling) PlayerPrefs.GetInt("Tunneling", 1);
        if (tunneling == Tunneling.ON)
        {
            vignette.SetActive(true);
        }
        else
        {
            vignette.SetActive(false);
        }
    }

    void Start()
    {
        LoadMovementType();
        LoadMovementSpeed();
        LoadTurnType();
        LoadTurnAngle();
        LoadTurnSpeed();
        LoadTunneling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
