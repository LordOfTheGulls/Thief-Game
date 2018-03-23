using UnityEngine;
 

[ExecuteInEditMode]
public class DoorDebugManager : MonoBehaviour
{
    private DoorController DoorController;

    private Vector2 MidPoint, FirstPoint, SecondPoint;
    private Vector3 LastDoorTwoPosition;

    private bool IsSelected;

    [Header("Debug Status:")]
    public bool LinkPreview;
    private string ControlDoorGizmo;
    private string ExitDoorGizmo;

    [Header("Transition Status:")]
    public bool IsDoorLocked;
    public bool IsPlayerInRange;
    public bool IsInTransition;

    [Header("Audio Status:")]
    public bool IsAudioDelayed;

    //[DOOR STATUS] 
    public void OnDoorTransitionStart(object source, Vector3 TargetLocation)
    {
        IsInTransition = true;
    }

    public void OnDoorTransitionEnd(object source, Vector3 TargetLocation)
    {
        IsInTransition = false;
    }

    //[DEBUG PREVIEW] 
    private void Start(){OnDrawGizmosSelected(); DoorController = GetComponent<DoorController>(); }
    private void Update(){if(IsInTransition) { OnDrawGizmosSelected(); } IsDoorLocked = DoorController.Locked; }

    private void OnDrawGizmos(){

        if (LinkPreview == true){

            if (DoorController.TargetSpawnPoint.gameObject.transform.position != LastDoorTwoPosition){

                OnDrawGizmosSelected();
            }
 
            if (IsSelected == true && IsDoorLocked == false){

                IsSelected = false; Gizmos.color = new Color(0, 255, 0, 0.3f);
            }
            else if(IsDoorLocked == true)
            {
                ControlDoorGizmo = "door_locked_point.png";
                ExitDoorGizmo = "door_locked_point.png";

                Gizmos.color = new Color(25, 25, 25, 0.15f);
            }
            else if(IsDoorLocked == false)
            {
                ControlDoorGizmo = "door_control_point.png";
                ExitDoorGizmo = "door_exit_point.png";

                Gizmos.color = new Color(255, 0, 0, 0.35f);
            }
                
            if (MidPoint.x > transform.GetChild(0).position.x + 1 || MidPoint.x < transform.GetChild(0).position.x - 1){

                DrawRectangleFormation(MidPoint, FirstPoint, DoorController.TargetSpawnPoint.gameObject.transform.position, transform.GetChild(0).position);
            }
            else{

                DrawLineFormation(transform.GetChild(0).position, DoorController.TargetSpawnPoint.gameObject.transform.position);
            }

            Gizmos.DrawIcon(transform.GetChild(0).position, ControlDoorGizmo, true);
            Gizmos.DrawIcon(DoorController.TargetSpawnPoint.gameObject.transform.position, ExitDoorGizmo, true);

            LastDoorTwoPosition = DoorController.TargetSpawnPoint.gameObject.transform.position;
        }
    }
    private void OnDrawGizmosSelected(){

        if (LinkPreview == true){

            DoorController = GetComponent<DoorController>();

            IsSelected = true;

            MidPoint = (DoorController.TargetSpawnPoint.gameObject.transform.position + transform.GetChild(0).position) / 2;
            MidPoint.x = DoorController.TargetSpawnPoint.gameObject.transform.position.x;

            FirstPoint = new Vector2(transform.GetChild(0).position.x, MidPoint.y);
            SecondPoint = new Vector2(DoorController.TargetSpawnPoint.gameObject.transform.position.x, DoorController.TargetSpawnPoint.gameObject.transform.position.y - MidPoint.y);
        }
    }

    private void DrawRectangleFormation(Vector2 Center, Vector2 FirstPoint, Vector2 Target, Vector2 Initial)
    {
        Gizmos.DrawLine(Initial, FirstPoint);
        Gizmos.DrawLine(Target, Center);
        Gizmos.DrawLine(FirstPoint, Center);
    }

    private void DrawLineFormation(Vector2 Initial, Vector2 Target)
    {
        Gizmos.DrawLine(Initial, Target);
    }

}