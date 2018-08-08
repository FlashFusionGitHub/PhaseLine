using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum UnitType { TANK, AATANK, HELICOPTER, BLIMP };

public class UnitActor : MonoBehaviour {
    //Units Type
    public UnitType m_type;

    [Header("Tank Settings")]
    //Sets Unit as a General
    public bool m_isGeneral;
    //Tanks Turret
    public GameObject m_turret;

    [Header ("Unit Settings")]
    //Units NavMeshAgent
    public NavMeshAgent m_agent;
    //Units Health Bar
    public Image m_healthBar;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
