using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTracking : MonoBehaviour
{
    public bool startAnimationFromRight;
    public float cameraSpeed;
    public float detectionDelay;
    public float On_Off_Delay;

    public Transform CameraTransf;
    public Transform PlayerTransf;

    private Vector2 A, B, C, RayDirection;

    private float cameraAngle;
    private float oppositeLen, adjacentLen;

    private Animator cameraAnimator;
    RaycastHit2D hit;
    private void Start()
    {
        A = new Vector2(CameraTransf.position.x, CameraTransf.position.y);

        cameraAnimator = GetComponent<Animator>();
        cameraAnimator.SetBool("StartDirection", startAnimationFromRight);

 
    }
    private void Update()
    {
        CameraTransf.rotation = cameraTracker(cameraSpeed);
 
    }

    private Quaternion cameraTracker(float cameraSpeed)
    {
        B = new Vector2(PlayerTransf.position.x, PlayerTransf.position.y);
        C = new Vector2(PlayerTransf.position.x, CameraTransf.position.y);

        oppositeLen = Vector2.Distance(A, C);
        adjacentLen = Vector2.Distance(A, B);

        cameraAngle = Mathf.Tan(oppositeLen / adjacentLen) * Mathf.Rad2Deg;
        Vector3 rotationVector = new Vector3(0, 90 - (C.x < A.x ? cameraAngle * -1 : cameraAngle), -30);
        Quaternion targetRotation = Quaternion.Euler(rotationVector);

        hit = Physics2D.Raycast(A, new Vector2(2 * cameraAngle, 3), 20);
        Debug.DrawRay(A, RayDirection, Color.yellow);

        return Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * cameraSpeed);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(A, B);
    }
}

 
