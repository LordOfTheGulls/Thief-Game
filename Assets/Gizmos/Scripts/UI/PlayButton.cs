using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {

        
	}
	
	// Update is called once per frame
	public void ButtonClick () {
        SceneManager.LoadScene(1);

    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

