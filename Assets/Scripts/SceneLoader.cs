using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;
public enum Scenes { MENU = 0, SPLITSCREEN, ONLINE, SETTINGS };

public class SceneLoader : MonoBehaviour {

    public InputDevice m_controller;

    public void LoadScene(int scene)
    {
        if(scene == (int)Scenes.SPLITSCREEN && InputManager.Devices.Count < 2)
        {
            Debug.Log("PLAYER 2 MISSING");
            return;
        }

        SceneManager.LoadScene(scene);
    }
}
