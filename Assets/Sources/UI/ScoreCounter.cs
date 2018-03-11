using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public Text scoreBoard;
	private int Score=0;

	public void ScoreCount(int givenPoints)
    {
        Score += givenPoints;
        scoreBoard.text = "Score: " + Score;
    }
}
