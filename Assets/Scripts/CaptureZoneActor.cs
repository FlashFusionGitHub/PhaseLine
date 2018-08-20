﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CaptureZoneActor : MonoBehaviour {

    public enum Owner { none, team1, team2};

    bool partialCaptureTeam1;
    bool partialCaptureTeam2;

    public float capturePercentage;

    public Owner owner;

    public float captureTimer = 0;
    public float captureTime = 10;

    List<TankActor> team1Tanks = new List<TankActor>();
    List<TankActor> team2Tanks = new List<TankActor>();

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (FindObjectOfType<GameStateManager>().isPaused == true)
            return;

        foreach (TankActor tank in team1Tanks.ToList())
        {
            if(tank == null)
                team1Tanks.Remove(tank);
        }

        foreach (TankActor tank in team2Tanks.ToList())
        {
            if(tank == null)
                team2Tanks.Remove(tank);
        }

        if (capturePercentage == 0)
        {
            owner = Owner.none;
        }

        if(team1Tanks.Count > 0 && team2Tanks.Count == 0)
        {
            if (partialCaptureTeam2)
            {
                capturePercentage = 0;
            }

            if (owner == Owner.none)
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
                        owner = Owner.team1;
                        FindObjectOfType<UnitManagerP1>().m_zonesCaptured.Add(this);
                    }
                }
            }
            else if(owner == Owner.team2)
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

        if (team2Tanks.Count > 0 && team1Tanks.Count == 0)
        {
            if (partialCaptureTeam1)
            {
                capturePercentage = 0;
            }

            if (owner == Owner.none)
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
                        owner = Owner.team2;
                        FindObjectOfType<UnitManagerP2>().m_zonesCaptured.Add(this);
                    }
                }
            }
            else if (owner == Owner.team1)
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

        if(team1Tanks.Count == 0 && team2Tanks.Count == 0 && owner == Owner.none)
        {
            capturePercentage = 0;
        }
	}

    private void OnTriggerEnter(Collider other) {

        if(other.GetComponent<TankActor>().m_team1unit)
        {
            team1Tanks.Add(other.GetComponent<TankActor>());
        }

        if (other.GetComponent<TankActor>().m_team2unit)
        {
            team2Tanks.Add(other.GetComponent<TankActor>());
        }
    }

    private void OnTriggerExit(Collider other) {

        if (other.GetComponent<TankActor>().m_team1unit)
        {
            team1Tanks.Remove(other.GetComponent<TankActor>());
        }

        if (other.GetComponent<TankActor>().m_team2unit)
        {
            team2Tanks.Remove(other.GetComponent<TankActor>());
        }
    }
}
