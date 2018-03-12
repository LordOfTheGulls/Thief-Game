//HARD-CODE FTW (TIME > CODE EFFICIENCY)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    DoorOpener SecondDoorScript;
    CameraMovement CameraScript;

    public GameObject FirstDoor;
    public GameObject SecondDoor;
    public GameObject TargetSpawnPoint;
    public float Range;

    private GameObject TargetDoor;

    private GameObject Player;

    private bool isPlaying;
    private bool Switch;

    private float Delay = 2;
    private bool ToggleSecondDoor;


    private bool Active;
    private bool Toggle;

    private void Start()
    {
        Toggle = true;
        TargetDoor = FirstDoor;

        SecondDoorScript = SecondDoor.GetComponent<DoorOpener>();
        CameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>() as CameraMovement;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector2 DoorPosition = TargetDoor.transform.position;
        Vector2 PlayerPosition = Player.transform.position;

        Vector2 RelativePosition = PlayerPosition - DoorPosition;

        if ((RelativePosition.x <= Range && RelativePosition.x >= -Range && RelativePosition.y <= Range && RelativePosition.y >= -Range) && Input.GetButton("Jump"))
        {
            Toggle = true;
            IsDoorOpened(true, gameObject);
        }


        if (isPlaying && Toggle == true)
        {

     
            Delay -= Time.deltaTime;
    
            if (Delay <= 0)
            {
                isPlaying = false;
                
                Active = !Active;
                Player.SetActive(false);
                IsDoorOpened(false, gameObject);
                IsDoorOpened(Active, SecondDoor);

                Delay = 3;

                if (!Active)
                {
                    Toggle = false; Player.transform.position = TargetSpawnPoint.transform.position; CameraScript.ChangePosition = true; Player.SetActive(true);
                }
            }
        }
    }
    public void IsDoorOpened(bool IsOpened, GameObject DoorObject)
    {
        DoorObject.GetComponent<Animator>().SetBool("CanOpen", IsOpened);
        DoorObject.GetComponent<Animator>().Play("Door Open");
        
        isPlaying = true;
    }

}