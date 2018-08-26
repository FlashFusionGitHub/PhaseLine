using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour {


    public List<TroopActor> allTroopActors = new List<TroopActor>();

    public List<TroopActor> team1Troops = new List<TroopActor>();
    public List<TroopActor> team2Troops = new List<TroopActor>();

    // Use this for initialization
    void Start () {
        FindAllTroopTargets();

        foreach (TroopActor troop in allTroopActors)
        {
            if (troop.team == Team.TEAM1 && troop.rankState == TroopActor.RankState.IsGeneral)
            {
                FindObjectOfType<TroopControllerP1>().AddGeneral(troop);
            }

            if (troop.team == Team.TEAM2 && troop.rankState == TroopActor.RankState.IsGeneral)
            {
                FindObjectOfType<TroopControllerP2>().AddGeneral(troop);
            }
        }
    }
	
    void FindAllTroopTargets()
    {
        allTroopActors = FindObjectsOfType<TroopActor>().ToList();
    }
}
