using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DoorAudioController))]
[RequireComponent(typeof(DoorDebugManager))]
public class DoorController : MonoBehaviour
{
    //[Door Settings]
    [Header("Door Options:")]
    public bool Locked; //'DropDownList' Types of Lock
    public float TransitionDelay;
    //public 'DropDownList' PuzzleType; < IF DOOR IS LOCKED.
    //public 'DropDownList' PuzzleDifficulty < IF DOOR IS LOCKED.

    [Header("Door Objects:")]
    public GameObject FirstDoor;
    public GameObject SecondDoor;
    [HideInInspector] public GameObject TargetSpawnPoint;

    [Header("Door Animation States - Names:")]
    public string OpenState;
    public string CloseState;
    public string StateParameter;

    //[Publisher - Event Handlers]
    private delegate void Door_Transition_Audio_EventHandler(object source, AudioSource AudioSource);
    private delegate void Door_Transition_EventHandler(object source, Vector3 TargetLocation);

    //[Publisher - Events - AUDIO]
    private event Door_Transition_Audio_EventHandler DoorTransitionAudioLocked;
    private event Door_Transition_Audio_EventHandler DoorTransitionAudioStart;
    private event Door_Transition_Audio_EventHandler DoorTransitionAudioEnd;

    //[Publisher - Events - TRANSITION]
    private event Door_Transition_EventHandler DoorTransitionLocked;
    private event Door_Transition_EventHandler DoorTransitionStart;
    private event Door_Transition_EventHandler DoorTransitionEnd;

    //[Script Dependancies]
    private DependancyManager DependancyManager;
    private DoorDebugManager DoorDebugManager;
    private DoorAudioController DoorAudioController;

    private PlayerController PlayerScript;
    private CameraController CameraScript;

    //[Main Controller Variables]
    private Animator DoorAnimator;

    private GameObject InitialDoor;
    private GameObject TargetDoor;

    private AudioSource TargetAudioSource;

    private bool CanOpen = true, CanClose, Swap;

    private float DoorBoundsMinX, DoorBoundsMaxX;
    private float DoorBoundsMinY, DoorBoundsMaxY;

    private void Awake()
    {
        //[Get Script Dependancies]
        DoorDebugManager = GetComponent<DoorDebugManager>();
        DoorAudioController = GetComponent<DoorAudioController>();

        PlayerScript = DependancyManager.PlayerController;
        CameraScript = DependancyManager.CamController;

        //[All Door-Transition Event's Subscribers]
        DoorTransitionEnd += DoorDebugManager.OnDoorTransitionEnd;
        DoorTransitionEnd += CameraScript.OnDoorTransitionEnd;
        DoorTransitionEnd += PlayerScript.OnDoorTransitionEnd;

        //[All Door-Transition Event's Subscribers]
        DoorTransitionStart += DoorDebugManager.OnDoorTransitionStart;
        DoorTransitionStart += CameraScript.OnDoorTransitionStart;
        DoorTransitionStart += PlayerScript.OnDoorTransitionStart;

        //[All Door-Transition AUDIO Event's Subscribers]
        DoorTransitionAudioLocked += DoorAudioController.OnDoorTransitionAudioLocked;
        DoorTransitionAudioStart += DoorAudioController.OnDoorTransitionAudioStart;
        DoorTransitionAudioEnd += DoorAudioController.OnDoorTransitionAudioEnd;

        //[Get Spawn-Point Dependancies]
        TargetSpawnPoint = SecondDoor.transform.GetChild(0).gameObject;
    }

    //[Main Script Methods]
    private void Start()
    {
        //Assign The Auxiliary Door Objects to their initial corresponding Objects.
        InitialDoor = FirstDoor;
        TargetDoor = SecondDoor;

        //Get Initial Door's Collider Bounds.
        GetCurrentBounds(GetComponent<BoxCollider2D>());

        //Get Initial Door's Audio Source.
        TargetAudioSource = InitialDoor.GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        //Check if the Player is in Range and the Script is not Already Running.
        if ((IsInRange() == true && CanOpen == true))
        {
            if (Input.GetButton("Jump") && Locked == false)
            {
                OpenDoor(true, InitialDoor, OpenState);
                CanOpen = false;
            }
            else if(Input.GetButton("Jump") && Locked == true)
            {
                OnDoorTransitionAudioLocked();
                OnDoorTransitionLocked();
            }
        }
    }

    //[Open the Second Door and Play Animations & Sounds]
    private IEnumerator TransitionWait(float Delay)
    {
        yield return new WaitForSeconds(TransitionDelay);
        OpenDoor(true, SecondDoor, OpenState);

        yield return new WaitForSeconds(Delay);
        CanOpen = true;
        OnDoorTransitionEnd();
        SwapDoors();
    }

    //[Close the Current Door and Play Animations & Sounds]
    private IEnumerator WaitForAnimation(GameObject DoorObject, Animator DoorAnimator, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        OnDoorTransitionStart();
        OpenDoor(false, DoorObject, CloseState);

        yield return new WaitForSeconds(Delay - 0.25f);
        OnDoorTransitionAudioEnd();

        if (CanOpen == false) { StartCoroutine(TransitionWait(Delay)); }
    }

    //[Open the Current Door and Play Animations & Sounds]
    private void OpenDoor(bool StateBool, GameObject DoorObject, string StateName)
    {
        TargetAudioSource = DoorObject.GetComponentInChildren<AudioSource>();
        DoorAnimator = DoorObject.GetComponent<Animator>();

        //Play the Specified Animation by it's Animator-State e.g. ('Door Open'), ('Door Close').
        DoorAnimator.SetBool(StateParameter, StateBool);
        DoorAnimator.Play(StateName);

        if (CanClose == true){CanClose = false;
    }
        else
        {
            OnDoorTransitionAudioStart();  
            StartCoroutine(WaitForAnimation(DoorObject, DoorAnimator, DoorAnimator.GetCurrentAnimatorStateInfo(0).length));
        }
    }

    //[Change Doors and Set the Current - Method]
    private void SwapDoors()
    {
        if ((Swap = !Swap) == false) { InitialDoor = FirstDoor; SecondDoor = TargetDoor; } else { InitialDoor = TargetDoor; SecondDoor = FirstDoor; }

        BoxCollider2D InitialDoorCollider = InitialDoor.GetComponent<BoxCollider2D>();

        GetCurrentBounds(InitialDoorCollider);
    }

    //[Get Current Door's Collider Bounds - Method]
    private void GetCurrentBounds(Collider2D Object)
    {
        DoorBoundsMinX = Object.bounds.min.x;
        DoorBoundsMaxX = Object.bounds.max.x;

        DoorBoundsMinY = Object.bounds.min.y;
        DoorBoundsMaxY = Object.bounds.max.y;
    }

    //['Is Player in Door's - Collider Range?' - Method]
    private bool IsInRange()
    {
        if ((((PlayerScript.PlayerPosition.x + PlayerScript.PlayerExtentsX) <= DoorBoundsMaxX
        && (PlayerScript.PlayerPosition.x - PlayerScript.PlayerExtentsX) >= DoorBoundsMinX)
        && ((PlayerScript.PlayerPosition.y + PlayerScript.PlayerExtentsY) <= DoorBoundsMaxY
        && (PlayerScript.PlayerPosition.y - PlayerScript.PlayerExtentsY) >= DoorBoundsMinY)))
        {
            DoorDebugManager.IsPlayerInRange = true;
            return true;
        }

        DoorDebugManager.IsPlayerInRange = false;
        return false;
    }

    //->[Event Methods - Audio Section]<-

    //[Audio Locked]
    protected virtual void OnDoorTransitionAudioLocked()
    {
        if (DoorTransitionAudioLocked != null)
        {
            DoorTransitionAudioLocked(this, TargetAudioSource);
        }
    }

    //[Audio Start]
    protected virtual void OnDoorTransitionAudioStart()
    {
        if (DoorTransitionAudioStart != null)
        {
            DoorTransitionAudioStart(this, TargetAudioSource);
        }
    }

    //[Audio End]
    protected virtual void OnDoorTransitionAudioEnd()
    {
        if (DoorTransitionAudioEnd != null)
        {
            DoorTransitionAudioEnd(this, TargetAudioSource);
        }
    }

    //->[Event Methods - Transition Section]<-

    //[Transition Locked]
    protected virtual void OnDoorTransitionLocked()
    {
        if (DoorTransitionLocked != null)
        {
            Vector3 TargetLocation = SecondDoor.transform.GetChild(0).transform.position;
            DoorTransitionLocked(this, TargetLocation);
        }
    }

    //[Transition Start]
    protected virtual void OnDoorTransitionStart()
    {
        CanClose = true;

        if (DoorTransitionStart != null)
        {
            Vector3 TargetLocation = SecondDoor.transform.GetChild(0).transform.position;
            DoorTransitionStart(this, TargetLocation);
        }
    }

    //[Transition End]
    protected virtual void OnDoorTransitionEnd()
    {

        if (DoorTransitionEnd != null)
        {
            Vector3 TargetLocation = SecondDoor.transform.GetChild(0).transform.position;
            DoorTransitionEnd(this, TargetLocation);
        }
    }
}