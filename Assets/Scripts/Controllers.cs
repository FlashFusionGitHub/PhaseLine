using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Controllers : MonoBehaviour
{

    public InputDevice m_controller1;
    public InputDevice m_controller2;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
        InputManager.OnDeviceAttached += controller1 => Controller1ConnectedMessage();
        InputManager.OnDeviceDetached += controller1 => ControllerDisconnectedMessage(m_controller1.Name);

        InputManager.OnDeviceAttached += controller2 => Controller2ConnectedMessage();
        InputManager.OnDeviceDetached += controller2 => ControllerDisconnectedMessage(m_controller2.Name);
    }

    void Controller1ConnectedMessage()
    {
        Debug.Log("Controller 1 Connected");

        m_controller1 = InputManager.Devices[0];
    }

    void Controller2ConnectedMessage()
    {
        Debug.Log("Controller 2 Connected");

        m_controller2 = InputManager.Devices[1];
    }

    void ControllerDisconnectedMessage(string name)
    {
        Debug.Log("Controller " + name + " Disconnected");
    }
}
