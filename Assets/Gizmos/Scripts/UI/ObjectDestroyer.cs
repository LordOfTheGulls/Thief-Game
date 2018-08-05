using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// vseki obekt koito dava score trqbva a ima tozi script; totalscore e ScoreCountera s GUI Text attachnat kum UI 

public class ObjectDestroyer : MonoBehaviour
{

    public ScoreCounter totalScore;
    public int Score;
    public float PickDelay;
    private GameObject player;
    private float TimeElapsed;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");          //tagni igracha
    }

    // Update is called once per frame
    void Update()
    {


            Vector2 relativePoint = gameObject.transform.position;
            Vector2 playerPoint = player.transform.position;
            double relativeX, relativeY;
            relativeX = playerPoint.x - relativePoint.x;
            relativeY = player.transform.position.y - gameObject.transform.position.y;

        if (Input.GetButton("Fire1") && (relativeX <= 1 && relativeX >= -1) && (relativeY <= 1 && relativeY >= -1))
        {
              TimeElapsed += Time.deltaTime;

             if (TimeElapsed >= PickDelay)
             {
                 Destroy(gameObject);
                 totalScore.ScoreCount(Score);
             }
             else
             {
              
             }
        }
        else
        {
            TimeElapsed = 0;
        }
    }
}
