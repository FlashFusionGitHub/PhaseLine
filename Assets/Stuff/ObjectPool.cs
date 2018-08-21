using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour {


    [SerializeField] public List<TroopActor> allTroopActors = new List<TroopActor>();

    // Use this for initialization
    void Start () {
        FindAllTroopTargets();

        foreach (TroopActor troop in allTroopActors)
        {
            if (troop.team == Team.TEAM1 && troop.rankState == TroopActor.RankState.IsGeneral)
            {
                FindObjectOfType<TroopController>().AddGeneralToList(troop);
            }
        }
    }
	
    void FindAllTroopTargets()
    {
        allTroopActors = FindObjectsOfType<TroopActor>().ToList();
    }
}
