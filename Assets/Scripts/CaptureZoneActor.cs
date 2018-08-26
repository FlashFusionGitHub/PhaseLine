using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CaptureZoneActor : MonoBehaviour {

    public enum Owner { NONE, TEAM1, TEAM2};

    bool partialCaptureTeam1;
    bool partialCaptureTeam2;

    public float capturePercentage;

    public Owner owner;

    public float captureTimer = 0;
    public float captureTime = 10;

    private ObjectPool op;

    // Use this for initialization
    void Start () {
        op = FindObjectOfType<ObjectPool>();
	}
	
	// Update is called once per frame
	void Update () {

        foreach (TroopActor tank in op.team1Troops)
        {
            if(tank == null)
                op.team1Troops.Remove(tank);
        }

        foreach (TroopActor tank in op.team2Troops.ToList())
        {
            if(tank == null)
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
                    partialCaptureTeam1 = true;
                    partialCaptureTeam2 = false;
                    capturePercentage += 10;
                    captureTimer = captureTime;

                    if (capturePercentage >= 100)
                    {
                        owner = Owner.TEAM1;
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
                    partialCaptureTeam1 = false;
                    partialCaptureTeam2 = true;
                    capturePercentage += 10;
                    captureTimer = captureTime;

                    if (capturePercentage >= 100)
                    {
                        owner = Owner.TEAM2;
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

        if(other.GetComponent<TroopActor>().team == Team.TEAM1)
        {
            op.team1Troops.Add(other.GetComponent<TroopActor>());
        }

        if (other.GetComponent<TroopActor>().team == Team.TEAM2)
        {
            op.team2Troops.Add(other.GetComponent<TroopActor>());
        }
    }

    private void OnTriggerExit(Collider other) {

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
