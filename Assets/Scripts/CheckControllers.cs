using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class CheckControllers : MonoBehaviour
{
    public InputDevice m_controller_1;
    public InputDevice m_controller_2;

    public Text m_controller_1_text;
    public Text m_controller_2_text;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputManager.OnDeviceAttached += m_controller_1 => Controller1Connected();
        InputManager.OnDeviceDetached += m_controller_1 => Controller1Disconnected();

        InputManager.OnDeviceAttached += m_controller_2 => Controller2Connected();
        InputManager.OnDeviceDetached += m_controller_2 => Controller2Disconnected();
    }

    void Controller1Connected()
    {
        m_controller_1 = InputManager.Devices[0];
        m_controller_1_text.enabled = false;
    }

    void Controller1Disconnected()
    {
        m_controller_1_text.enabled = true;
        GetComponent<GameStateManager>().isPaused = true;
    }

    void Controller2Connected()
    {
        m_controller_2 = InputManager.Devices[1];
        m_controller_2_text.enabled = false;
    }

    void Controller2Disconnected()
    {
        m_controller_2_text.enabled = true;
        GetComponent<GameStateManager>().isPaused = true;
    }
}
