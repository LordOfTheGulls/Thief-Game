using System.Collections;
using System.Collections.Generic;
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
        if (ChangePosition == true)
        {
            YTarget = Player.transform.position.y;
            ChangePosition = false;
        }
  
    }
    private void FixedUpdate(){
            Vector3 Target = new Vector3(Player.transform.position.x, YTarget, gameObject.transform.position.z);
            transform.position = Vector3.Lerp(new Vector3(transform.position.x + CameraOffset.x / 10, transform.position.y + CameraOffset.y / 10, transform.position.z), Target, FollowSpeed * Time.deltaTime);
    }
}
