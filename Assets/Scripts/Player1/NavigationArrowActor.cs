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

    private InputDevice m_controller;

    public Material markerMaterial;

    [SerializeField] private float m_MinXPos = -100.0f, m_MaxXPos = 100.0f;
    [SerializeField] private float m_MinZPos = -100.0f, m_MaxZPos = 100.0f;

    public int markerSpeed = 2;

    public Team team;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        if(team == Team.TEAM1)
        {
            m_controller = FindObjectOfType<Controllers>().m_controller1;

            transform.position += new Vector3(m_controller.LeftStickX, 0, m_controller.LeftStickY) * markerSpeed;
        }

        if (team == Team.TEAM2)
        {
            m_controller = FindObjectOfType<Controllers>().m_controller2;

            transform.position += new Vector3(-m_controller.LeftStickX, 0, -m_controller.LeftStickY) / markerSpeed;
        }

        float markerXPos = Mathf.Clamp(transform.position.x, m_MinXPos, m_MaxXPos);
        float markerZPos = Mathf.Clamp(transform.position.z, m_MinZPos, m_MaxZPos);

        transform.position = new Vector3(markerXPos, transform.position.y, markerZPos);
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
