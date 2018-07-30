using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//consider creating 1 class for every vehicle
public class CopperActor : MonoBehaviour {

    public NavMeshAgent m_agent;

    //Available teams
    public bool m_team1unit;
    public bool m_team2unit;

    //copper health
    public float m_starthealth = 100;
    private float m_health;

    //copper attack time
    public float m_AttackTime;
    private float m_AttackTimer = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack(TankActor tank, CopperActor copper) {

    }
}
