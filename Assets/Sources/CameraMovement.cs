using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private Camera MainCamera;

    public bool ChangePosition;
    private GameObject Player;

	private void Start () {
        MainCamera = Camera.main;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

	private void Update () {
        if (ChangePosition)
        {
            gameObject.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, gameObject.transform.position.z);
            ChangePosition = false;
        }
    }
    private void FixedUpdate(){

        Vector3 Target = new Vector3(Player.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), Target, 3.5f * Time.deltaTime);
    }
}
