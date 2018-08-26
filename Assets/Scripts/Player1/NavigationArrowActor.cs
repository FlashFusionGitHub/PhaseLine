using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    NONE, TEAM1, TEAM2
};

public class NavigationArrowActor : MonoBehaviour {

    public TankActor m_tank;

    public GameObject AirStrikeMarker;
    public GameObject NavigationMarker;

    public GameObject AirStrike;

    public Material markerMaterial;

    public float m_MinXPos = -100.0f, m_MaxXPos = 100.0f;
    public float m_MinZPos = -100.0f, m_MaxZPos = 100.0f;

    public int markerSpeed = 2;

    public Team team;

    public GameObject currentMarker;

    public int team1AirStrikes = 0;
    public int team2AirStrikes = 0;

    public bool airStrikeState;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

    }
}
