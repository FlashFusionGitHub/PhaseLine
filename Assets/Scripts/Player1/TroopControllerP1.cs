using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class TroopControllerP1 : TroopController {

    public Controllers m_controllers;
    public NavigationArrowP1 navigationArrow;

    // Use this for initialization
    protected override void Start () {
        m_navigationArrowActor = navigationArrow;

        base.Start();
    }

    // Update is called once per frame
    protected override void Update() {
        m_controller = m_controllers.m_controller1;

        base.Update();
    }
}
