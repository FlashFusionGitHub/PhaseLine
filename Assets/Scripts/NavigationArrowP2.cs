using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class NavigationArrowP2 : NavigationArrowActor
{
    private InputDevice m_controller;

    // Use this for initialization
    void Start () {
        currentMarker = Instantiate(NavigationMarker, new Vector3(0, 4, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        m_controller = FindObjectOfType<Controllers>().m_controller2;

        foreach (CaptureZoneActor zone in FindObjectOfType<ZoneController>().zones)
        {
            if (zone.owner == CaptureZoneActor.Owner.TEAM1)
            {
                team1AirStrikes++;
            }
        }

        float markerXPos = Mathf.Clamp(transform.position.x, m_MinXPos, m_MaxXPos);
        float markerZPos = Mathf.Clamp(transform.position.z, m_MinZPos, m_MaxZPos);

        transform.position = new Vector3(markerXPos, transform.position.y, markerZPos);

        currentMarker.transform.position += new Vector3(m_controller.LeftStickX, 0, m_controller.LeftStickY) * markerSpeed;

        AirStrikeControls();
    }

    public void AirStrikeControls()
    {
        if (m_controller.Action3.WasPressed && !airStrikeState)
        {
            EnableAirStrikeMarker();
        }
        else if (m_controller.Action3.WasPressed && airStrikeState)
        {
            EnableNavigationMarker();
        }

        if (airStrikeState && m_controller.Action1.WasPressed)
        {
            Instantiate(AirStrike, currentMarker.transform.position, currentMarker.transform.rotation);
            EnableNavigationMarker();
        }
    }

    void EnableAirStrikeMarker()
    {
        airStrikeState = true;
        GameObject prevMarker = currentMarker;
        currentMarker = Instantiate(AirStrikeMarker, new Vector3(prevMarker.transform.position.x, 1, prevMarker.transform.position.z), Quaternion.identity);
        Destroy(prevMarker);
    }

    void EnableNavigationMarker()
    {
        airStrikeState = false;
        GameObject prevMarker = currentMarker;
        currentMarker = Instantiate(NavigationMarker, new Vector3(prevMarker.transform.position.x, 4, prevMarker.transform.position.z), Quaternion.identity);
        Destroy(prevMarker);
    }

    public TankActor GetEnemyToAttack()
    {
        if (m_tank != null)
            return m_tank;
        else
            return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (team == Team.TEAM2)
        {
            if (other.gameObject.GetComponent<TankActor>().m_team2unit)
            {
                m_tank = other.GetComponent<TankActor>();
                markerMaterial.color = Color.red;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (team == Team.TEAM2)
        {
            if (other.gameObject.GetComponent<TankActor>().m_team2unit)
            {
                m_tank = null;
                markerMaterial.color = Color.green;
            }
        }
    }
}
