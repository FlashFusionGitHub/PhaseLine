using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerP1 : CameraController {

    public Controllers m_controllers;

    // Use this for initialization
    protected override void Start()
    {
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        m_controller = m_controllers.m_controller1;

        base.Update();
    }
}
