using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    NONE, TEAM1, TEAM2
};

public class NavigationArrowActor : MonoBehaviour {

    private TankActor m_tank;

    public GameObject AirStrikeMarker;
    public GameObject NavigationMarker;

    public GameObject AirStrike;

    private InputDevice m_controller;

    public Material markerMaterial;

    [SerializeField] private float m_MinXPos = -100.0f, m_MaxXPos = 100.0f;
    [SerializeField] private float m_MinZPos = -100.0f, m_MaxZPos = 100.0f;

    public int markerSpeed = 2;

    public Team team;

    public GameObject currentMarker;

    int team1AirStrikes = 0;
    int team2AirStrikes = 0;

    public bool airStrikeState;

    // Use this for initialization
    void Start () {
        currentMarker = Instantiate(NavigationMarker, new Vector3(0, 4, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {

        foreach (CaptureZoneActor zone in FindObjectOfType<ZoneController>().zones)
        {
            if(zone.owner == CaptureZoneActor.Owner.TEAM1)
            {
                team1AirStrikes++;
            }

            if (zone.owner == CaptureZoneActor.Owner.TEAM2)
            {
                team2AirStrikes++;
            }
        }

        if(team == Team.TEAM1)
        {
            m_controller = FindObjectOfType<Controllers>().m_controller1;

            currentMarker.transform.position += new Vector3(m_controller.LeftStickX, 0, m_controller.LeftStickY) * markerSpeed;

            if(m_controller.Action3.WasPressed && !airStrikeState)
            {
                EnableAirStrikeMarker();
            }
            else if (m_controller.Action3.WasPressed && airStrikeState)
            {
                EnableNavigationMarker();
            }

            if(airStrikeState && m_controller.Action1.WasPressed)
            {
                Instantiate(AirStrike, currentMarker.transform.position, currentMarker.transform.rotation);
                EnableNavigationMarker();
            }
        }

        if (team == Team.TEAM2)
        {
            m_controller = FindObjectOfType<Controllers>().m_controller2;

            currentMarker.transform.position += new Vector3(-m_controller.LeftStickX, 0, -m_controller.LeftStickY) / markerSpeed;
        }

        float markerXPos = Mathf.Clamp(transform.position.x, m_MinXPos, m_MaxXPos);
        float markerZPos = Mathf.Clamp(transform.position.z, m_MinZPos, m_MaxZPos);

        transform.position = new Vector3(markerXPos, transform.position.y, markerZPos);
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
        if(team == Team.TEAM1)
        {
            if (other.gameObject.GetComponent<TankActor>().m_team2unit)
            {
                m_tank = other.GetComponent<TankActor>();
                markerMaterial.color = Color.red;
            }
        }

        if (team == Team.TEAM2)
        {
            if (other.gameObject.GetComponent<TankActor>().m_team1unit)
            {
                m_tank = other.GetComponent<TankActor>();
                markerMaterial.color = Color.red;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (team == Team.TEAM1)
        {
            if (other.gameObject.GetComponent<TankActor>().m_team2unit)
            {
                m_tank = null;
                markerMaterial.color = Color.green;
            }
        }

        if (team == Team.TEAM2)
        {
            if (other.gameObject.GetComponent<TankActor>().m_team1unit)
            {
                m_tank = null;
                markerMaterial.color = Color.green;
            }
        }
    }
}
