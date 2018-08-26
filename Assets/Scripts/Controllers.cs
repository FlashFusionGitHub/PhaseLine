using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Controllers : MonoBehaviour
{

    public InputDevice m_controller1;
    public InputDevice m_controller2;
	
	// Update is called once per frame
	void Update ()
    {
        m_controller1 = InputManager.Devices[0];
        m_controller2 = InputManager.Devices[1];
    }
}
