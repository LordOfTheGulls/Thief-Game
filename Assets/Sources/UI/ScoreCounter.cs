using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public GameObject ScoreWin;
    public Text scoreBoard;
	private int Score=0;

    public void Update()
    {
        if(ScoreWin.active == true)
        {
            ScoreWin.GetComponent<Text>().text = scoreBoard.text + " SCORE !";
        }
    }
    public void ScoreCount(int givenPoints)
    {
        Score += givenPoints;
        scoreBoard.text = Score.ToString();
    }
}
