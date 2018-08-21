using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public Team team;

    public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(team == Team.TEAM1)
        {
            if (other.gameObject.GetComponent<TankActor>().m_team2unit)
            {
                other.gameObject.GetComponent<TankActor>().TakeDamage(damage);
            }
        }

        if (team == Team.TEAM2)
        {
            if (other.gameObject.GetComponent<TankActor>().m_team1unit)
            {
                other.gameObject.GetComponent<TankActor>().TakeDamage(damage);
            }
        }

        if (team == Team.NONE)
        {
            if(other.gameObject.GetComponent<TankActor>())
            {
                other.gameObject.GetComponent<TankActor>().TakeDamage(damage);
            }
        }
    }
}
