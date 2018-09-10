using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class CaptureZoneActor : MonoBehaviour {

    public enum Owner { NONE, TEAM1, TEAM2};

    bool partialCaptureTeam1;
    bool partialCaptureTeam2;

    [Header ("Owner")]
    public Owner owner;

    [Header ("Capture timer")]
    public float captureTimer = 0;
    public float captureTime = 10;
    public float capturePercentage;

    [Header ("Object Pool")]
    private ObjectPool op;

    [Header ("Capture Events Team 1")] // events for when team 1 starts capturing a zone
    public UnityEvent onStartCaptureTeam1;
    public UnityEvent onCaptureTeam1;

    [Header ("Capture Events Team 2")] // events for when team 2 starts capturing a zone
    public UnityEvent onStartCaptureTeam2;
    public UnityEvent onCaptureTeam2;
    // Use this for initialization
    void Start () {
        op = FindObjectOfType<ObjectPool>();
	}
	
	// Update is called once per frame
	void Update () {

        foreach (TroopActor tank in op.team1Troops)
        {
            if(!tank.gameObject.activeInHierarchy || Vector3.Distance (transform.position, tank.transform.position) > transform.lossyScale.x) //changed from (!tank)
                op.team1Troops.Remove(tank);
        }

        foreach (TroopActor tank in op.team2Troops.ToList())
        {
            if(!tank.gameObject.activeInHierarchy || Vector3.Distance(transform.position, tank.transform.position) > transform.lossyScale.x) //changed from (!tank)
                op.team2Troops.Remove(tank);
        }

        if (capturePercentage == 0)
        {
            owner = Owner.NONE;
        }

        if(op.team1Troops.Count > 0 && op.team2Troops.Count == 0)
        {
            if (partialCaptureTeam2)
            {
                capturePercentage = 0;
            }

            if (owner == Owner.NONE)
            {
                captureTimer -= Time.deltaTime;

                if (captureTimer <= 0)
                {
                    onStartCaptureTeam1.Invoke(); //added event to plug in effects and sounds
                    partialCaptureTeam1 = true;
                    partialCaptureTeam2 = false;
                    capturePercentage += 10;
                    captureTimer = captureTime;

                    if (capturePercentage >= 100)
                    {
                        owner = Owner.TEAM1;
                        onCaptureTeam1.Invoke(); //added event to plug in effects and sounds
                    }
                }
            }
            else if(owner == Owner.TEAM2)
            {
                captureTimer -= Time.deltaTime;

                if (captureTimer <= 0)
                {
                    capturePercentage -= 10;
                    captureTimer = captureTime;
                    onStartCaptureTeam1.Invoke(); //added event to plug in effects and sounds
                }
            }
            else
            {
                return;
            }
        }

        if (op.team2Troops.Count > 0 && op.team1Troops.Count == 0)
        {
            if (partialCaptureTeam1)
            {
                capturePercentage = 0;
            }

            if (owner == Owner.NONE)
            {
                captureTimer -= Time.deltaTime;

                if (captureTimer <= 0)
                {
                    onStartCaptureTeam2.Invoke(); //added event to plug in effects and sounds
                    partialCaptureTeam1 = false;
                    partialCaptureTeam2 = true;
                    capturePercentage += 10;
                    captureTimer = captureTime;

                    if (capturePercentage >= 100)
                    {
                        owner = Owner.TEAM2;
                        onCaptureTeam2.Invoke(); //added Event to plug in effects and sounds
                    }
                }
            }
            else if (owner == Owner.TEAM1)
            {
                captureTimer -= Time.deltaTime;

                if (captureTimer <= 0)
                {
                    capturePercentage -= 10;
                    captureTimer = captureTime;
                    onStartCaptureTeam2.Invoke(); //added event to plug in effects and sounds
                }
            }
            else
            {
                return;
            }
        }

        if(op.team1Troops.Count == 0 && op.team2Troops.Count == 0 && owner == Owner.NONE)
        {
            capturePercentage = 0;
        }
	}

    private void OnTriggerEnter(Collider other) {

        TroopActor currentActor = other.GetComponent<TroopActor>();
        if (currentActor)
        {
           // if (other.GetComponent<Explosion>() == true)
           // {
           //     return;
           // }

            if (other.GetComponent<TroopActor>().team == Team.TEAM1)
            {
                op.team1Troops.Add(other.GetComponent<TroopActor>());
            }
            else if (other.GetComponent<TroopActor>().team == Team.TEAM2)
            {
                op.team2Troops.Add(other.GetComponent<TroopActor>());
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {

        TroopActor currentActor = other.GetComponent<TroopActor>();
        if (currentActor)
        {
            if (other.GetComponent<TroopActor>().team == Team.TEAM1)
            {
                op.team1Troops.Remove(other.GetComponent<TroopActor>());
            }

            if (other.GetComponent<TroopActor>().team == Team.TEAM2)
            {
                op.team2Troops.Remove(other.GetComponent<TroopActor>());
            }
        }
    }
}
