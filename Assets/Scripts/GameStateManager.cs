using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

    public bool isPaused;
    public Text pauseText;

	// Use this for initialization
	void Start ()
    {
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isPaused)
        {

        }
    }

    void PauseSwitch()
    {
        if (isPaused == false)
        {
            pauseText.enabled = true;
            isPaused = true;
        }
        if (isPaused == true)
        {
            pauseText.enabled = true;
            isPaused = false;
        }
    }
}
