using UnityEngine;

[ExecuteInEditMode]
public class DoorDebugManager : MonoBehaviour
{
    private DoorController DoorController;

    private Vector2 MidPoint, FirstPoint, SecondPoint;
    private Vector3 LastDoorTwoPosition;

    private bool IsSelected;

    [Header("Debug Status:")]
    public bool DebugPreview;

    [Header("Door Transition Status:")]
    public bool IsPlayerInRange;
    public bool InTransition;

    [Header("Door Audio Status:")]
    public bool IsAudioDelayed;

    //[DOOR STATUS] 
    public void OnDoorTransitionStart(object source, Vector3 TargetLocation)
    {
        InTransition = true;
    }

    public void OnDoorTransitionEnd(object source, Vector3 TargetLocation)
    {
        InTransition = false;
    }

    //[DEBUG PREVIEW] 
    private void Start(){OnDrawGizmosSelected();}
    private void Update(){if(InTransition) { OnDrawGizmosSelected(); }}

    private void OnDrawGizmos(){

        if (DebugPreview == true){

            if (DoorController.TargetSpawnPoint.gameObject.transform.position != LastDoorTwoPosition){

                OnDrawGizmosSelected();
            }
 
            if (IsSelected == true){
                IsSelected = false; Gizmos.color = new Color(0, 255, 0, 0.3f);
            }
            else
                Gizmos.color = new Color(255, 0, 0, 0.3f);

            if (MidPoint.x > transform.GetChild(0).position.x + 1 || MidPoint.x < transform.GetChild(0).position.x - 1){

                DrawRectangleFormation(MidPoint, FirstPoint, DoorController.TargetSpawnPoint.gameObject.transform.position, transform.GetChild(0).position);
            }
            else{

                DrawLineFormation(transform.GetChild(0).position, DoorController.TargetSpawnPoint.gameObject.transform.position);
            }

                Gizmos.DrawIcon(transform.GetChild(0).position, "door_control_point.png", true);
                Gizmos.DrawIcon(DoorController.TargetSpawnPoint.gameObject.transform.position, "door_exit_point.png", true);

            LastDoorTwoPosition = DoorController.TargetSpawnPoint.gameObject.transform.position;
        }
    }
    private void OnDrawGizmosSelected(){

        if (DebugPreview == true){

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