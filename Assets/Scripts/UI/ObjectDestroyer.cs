using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// vseki obekt koito dava score trqbva a ima tozi script; totalscore e ScoreCountera s GUI Text attachnat kum UI 

public class ObjectDestroyer : MonoBehaviour
{

    public ScoreCounter totalScore;
    public int Score;
    private GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");          //tagni igracha
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1"))    // left ctrl
        {
            Vector2 relativePoint = gameObject.transform.position;
            Vector2 playerPoint = player.transform.position;
            double relativeX, relativeY;
            relativeX = playerPoint.x - relativePoint.x;
            relativeY = player.transform.position.y - gameObject.transform.position.y;

            if ((relativeX <= 1 && relativeX >= -1) && (relativeY <= 1 && relativeY >= -1))
            {
                Destroy(gameObject);
                totalScore.ScoreCount(Score);
            }
        }
    }

}
