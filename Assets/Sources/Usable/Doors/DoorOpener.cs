using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public AudioClip[] DoorSounds;
    private AudioSource DoorAudio;

    public static DependancyManager DependancyManager;
    public static PlayerController PlayerScript;
    public static CameraController CameraScript;

    public bool DebugPreview;
    public float TransitionDelay;

    public GameObject FirstDoor;
    public GameObject SecondDoor;

    public GameObject TargetSpawnPoint;

    private GameObject TargetDoor;
    private GameObject Player;
    private GameObject Temp;

    private Collider2D DoorBounds1, DoorBounds2;

    private bool isPlaying, Active, Toggle, IsPlayerInRange, Swap, ChangeGizmoColour;
    private float Range, Delay, DoorBoundsMinX, DoorBoundsMaxX, DoorBoundsMaxY, DoorBoundsMinY;

    private void Start()
    {
        Toggle = true;
        Delay = TransitionDelay;

        TargetDoor = FirstDoor;
        Temp = SecondDoor;

        Player = DependancyManager.PlayerObject;
        PlayerScript = DependancyManager.PlayerController;
        CameraScript = DependancyManager.CamController;

        DoorBounds1 = GetComponent<BoxCollider2D>();
        DoorBounds2 = SecondDoor.GetComponent<BoxCollider2D>();
        DoorAudio = Player.GetComponent<AudioSource>();

        GetCurrentBounds(DoorBounds1);
    }

    private void Update()
    {
        if (IsInRange() == true  && Input.GetButton("Jump"))
        {
            Toggle = true;
            IsDoorOpened(true, TargetDoor);
        }

        if (isPlaying && Toggle == true)
        {
            Delay -= Time.deltaTime;

            if (Delay <= 0)
            {
                Active = !Active;
                isPlaying = false;
                Player.SetActive(false);
                IsDoorOpened(false, TargetDoor);   IsDoorOpened(Active, SecondDoor);
 
                if (!Active)
                {
                    Swap = !Swap;
                    Toggle = false;

                    Player.transform.position = SecondDoor.transform.GetChild(0).position;
                    Player.SetActive(true);
                    PlayerScript.isPlayerControllable = true;
                    CameraScript.ChangePosition = true;
                    DoorAudio.PlayOneShot(DoorSounds[0], 0.7f);
                }

                SwapDoors(Swap);
                Delay = TransitionDelay;
            }
        }
    }

    public void IsDoorOpened(bool IsOpened, GameObject DoorObject)
    {
        DoorObject.GetComponent<Animator>().SetBool("CanOpen", IsOpened);
        DoorObject.GetComponent<Animator>().Play("Door Open");

        isPlaying = true;

        PlayerScript.isPlayerControllable = false;
    }

    public void SwapDoors(bool CanSwap)
    {
        if (CanSwap == false){TargetDoor = FirstDoor;   SecondDoor = Temp;} else{TargetDoor = Temp; SecondDoor = FirstDoor;}

        GetCurrentBounds(TargetDoor.GetComponent<BoxCollider2D>());
    }

    public void GetCurrentBounds(Collider2D Object)
    {
        DoorBoundsMinX = Object.bounds.min.x; DoorBoundsMaxX = Object.bounds.max.x;
        DoorBoundsMinY = Object.bounds.min.y; DoorBoundsMaxY = Object.bounds.max.y;
    }

    private bool IsInRange()
    {
        if ((((PlayerScript.PlayerPosition.x + PlayerScript.PlayerExtentsX) <= DoorBoundsMaxX
        && (PlayerScript.PlayerPosition.x - PlayerScript.PlayerExtentsX) >= DoorBoundsMinX)
        && ((PlayerScript.PlayerPosition.y + PlayerScript.PlayerExtentsY) <= DoorBoundsMaxY
        && (PlayerScript.PlayerPosition.y - PlayerScript.PlayerExtentsY) >= DoorBoundsMinY)))
        {
            IsPlayerInRange = true;
            return true;
        }
        IsPlayerInRange = false;
        return false;
    }

    //[DEBUG PREVIEW]
    private void OnDrawGizmos()
    {
        if(DebugPreview == true && transform.position != null && SecondDoor != null)
        {
            if(ChangeGizmoColour == true)
            {
                Gizmos.color = new Color(0, 0, 255, 0.3f);
                ChangeGizmoColour = false;
            }        
            else
                Gizmos.color = new Color(255, 0, 0, 0.3f);

            Gizmos.DrawLine(transform.GetChild(0).position, TargetSpawnPoint.transform.position);

            Gizmos.DrawIcon(transform.GetChild(0).position, "door_control_point.png", true);
            Gizmos.DrawIcon(TargetSpawnPoint.transform.position, "door_exit_point.png", true);
        }
    }
    private void OnDrawGizmosSelected()
    {
        ChangeGizmoColour = true;
    }
}