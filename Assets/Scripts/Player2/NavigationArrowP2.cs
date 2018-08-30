using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class NavigationArrowP2 : NavigationArrowActor {

    int airStrikes;

    // Use this for initialization
    protected override void Start () {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update () {
        m_controller = FindObjectOfType<Controllers>().m_controller2;

        foreach (CaptureZoneActor zone in FindObjectOfType<ZoneController>().zones) {
            if (zone.owner == CaptureZoneActor.Owner.TEAM2) {
                airStrikes++;
            }
        }

        base.Update();
    }

    void OnTriggerEnter(Collider other) {
            if (other.gameObject.GetComponent<TroopActor>().team == Team.TEAM1) {
                m_tank = other.GetComponent<TroopActor>();
                //m_markerMaterial.color = Color.red;
            }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<TroopActor>().team == Team.TEAM1)
        {
            m_tank = other.GetComponent<TroopActor>();
            //m_markerMaterial.color = Color.green;
        }
    }
}
