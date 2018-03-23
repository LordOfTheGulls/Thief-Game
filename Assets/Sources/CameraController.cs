using UnityEngine;

public class CameraController : MonoBehaviour {

    public float FollowSpeed;
    public bool ChangePosition;

    public Vector2 CameraOffset;
    public float YTarget;

    private Camera MainCamera;
    private GameObject Player;

    private void Start () {

        MainCamera = Camera.main;
        Player = DependancyManager.PlayerObject;
    }
    private void Update()
    {
        if(ChangePosition)
        {
            ChangePosition = false;
            YTarget = Player.transform.position.y;
        }
    }
    private void FixedUpdate(){
            Vector3 Target = new Vector3(Player.transform.position.x, YTarget, gameObject.transform.position.z);
            transform.position = Vector3.Lerp(new Vector3(transform.position.x + CameraOffset.x / 10, transform.position.y + CameraOffset.y / 10, transform.position.z), Target, FollowSpeed * Time.deltaTime);
    }

    public void OnDoorTransitionStart(object source, Vector3 TargetLocation)
    {
        
    }

    public void OnDoorTransitionEnd(object source, Vector3 TargetLocation)
    {
        ChangePosition = true;
    }
}
