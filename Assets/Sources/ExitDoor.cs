using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour {

    public GameObject WinObject;
    public float Range;
    private GameObject Player;
    public GameObject Timer;
    public float Delay;
    private bool isPlaying;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update () {
        Vector2 DoorPosition = gameObject.transform.position;
        Vector2 PlayerPosition = Player.transform.position;

        Vector2 RelativePosition = PlayerPosition - DoorPosition;

        if ((RelativePosition.x <= Range && RelativePosition.x >= -Range && RelativePosition.y <= Range && RelativePosition.y >= -Range) && Input.GetButton("Jump"))
        {
            IsDoorOpened(true, gameObject);
        }
        if (isPlaying)
        {
            Delay -= Time.deltaTime;

            if (Delay <= 0)
            {
                IsDoorOpened(false, gameObject);
                Player.SetActive(false);

                Player.GetComponent<PlayerController>().enabled = false;
                Timer.GetComponent<Timer>().enabled = false;


                isPlaying = false;
                WinObject.active = true;

                Delay = 0;
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
