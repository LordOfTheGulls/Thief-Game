using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour{

    DoorOpener SecondDoorScript;
    public GameObject SecondDoor;

    public float Range;

    public GameObject Player;

    public bool Transition;

    public int Counter;

    private void Start()
    {
        SecondDoorScript = SecondDoor.GetComponent<DoorOpener>();
    }

    private void Update()
    {
        if(Input.GetButton("Jump"))
        {
            Vector2 DoorPosition = gameObject.transform.position;
            Vector2 PlayerPosition = Player.transform.position;

            Vector2 RelativePosition = PlayerPosition - DoorPosition;

            if((RelativePosition.x <= Range && RelativePosition.x >= -Range) 
            && (RelativePosition.y <= Range && RelativePosition.y >=0))
            {
                Counter = 0;
                IsDoorOpened(true, gameObject);
            }
        }
    }
    IEnumerator Delay(GameObject DoorOjbect)
    {
        yield return new WaitForSeconds(2);

        IsDoorOpened(false, DoorOjbect);
        
        SecondDoorScript.IsDoorOpened(true, SecondDoor);

        //Toggle Second Door from 1st to second and second to 1st

    }

    public void IsDoorOpened(bool IsOpened, GameObject DoorObject)
    {
        Counter++;

        DoorObject.GetComponent<Animator>().SetBool("CanOpen", IsOpened);
        DoorObject.GetComponent<Animator>().Play("Door Open");

        if(Counter <= 1)
         StartCoroutine(Delay(DoorObject));
    }


}
