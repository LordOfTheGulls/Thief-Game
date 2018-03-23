using UnityEngine;

[RequireComponent(typeof(FogManager))]

public class DependancyManager : MonoBehaviour
{
    public static FogManager FogController;
    //Scripts
    public static PlayerController PlayerController;
    public static CameraController CamController;

    //Layermasks

    //Level objects
    public static GameObject PlayerObject;
    public static Vector3 SpawnLocation;

    //Unity Components
    public static Animator PlayerAnimator;
    public static AudioSource PlayerAudio;
    public static Rigidbody2D PlayerRigidBody;
    public static CapsuleCollider2D PlayerCollider;

    //State
    public static bool DependanciesLoaded;

    void Awake()
    {
        //Dependancies
        PlayerObject = GameObject.FindWithTag("Player");
        //SpawnLocation = GameObject.FindWithTag("Spawn").transform.position;

        PlayerController = PlayerObject.GetComponent<PlayerController>();
        CamController = Camera.main.GetComponent<CameraController>();

        FogController = gameObject.GetComponent<FogManager>();

        PlayerAnimator = PlayerObject.GetComponent<Animator>();
        PlayerRigidBody = PlayerObject.GetComponent<Rigidbody2D>();
        PlayerCollider = PlayerObject.GetComponent<CapsuleCollider2D>();

        //Component Assignment Checks
        //if (PlatformsLayer.value.ToString() == "Nothing" || PickableLayer.value.ToString() == "Nothing") { Debug.Log("[DEPENDANCY ERROR]LayerMasks are missing!"); return; }
        if (PlayerObject == null) { Debug.Log("[DEPENDANCY ERROR] An Object with the tag (Player), hasn't been found!"); return; }
        //if (SpawnLocation == null) { Debug.Log("[DEPENDANCY ERROR] An Object with the tag (Spawn), hasn't been found!"); return; }
        if (PlayerController == null) { Debug.Log("[DEPENDANCY ERROR] A Script with the name (Controller), hasn't been found!"); return; }
        

        if (PlayerAnimator == null) { Debug.Log("[DEPENDANCY ERROR] A Component with the name  (PlayerAnimator), hasn't been found!"); return; }
        //if (PlayerAudio == null) { Debug.Log("[DEPENDANCY ERROR] A Component with the name  (PlayerAudio), hasn't been found!"); return; }
        if (PlayerRigidBody == null) { Debug.Log("[DEPENDANCY ERROR] A Component with the name  (PlayerRigidBody), hasn't been found!"); return; }
        if (PlayerCollider == null) { Debug.Log("[DEPENDANCY ERROR] A Component with the name  (PlayerCollider), hasn't been found!"); return; }

        DependanciesLoaded = true;

        Debug.Log("<color=blue>[Dependancies for this Scene has been SUCCESSFULLY LOADED!]</color>");
    }
}
