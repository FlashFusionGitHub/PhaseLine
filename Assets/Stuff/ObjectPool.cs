using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour {


    public List<TroopActor> allTroopActors = new List<TroopActor>();

    public List<TroopActor> team1Troops = new List<TroopActor>();
    public List<TroopActor> team2Troops = new List<TroopActor>();

    public TroopControllerP1 m_troopControllerP1;
    public TroopControllerP2 m_troopControllerP2;

    // Use this for initialization
    void Start () {
        FindAllTroopTargets();
        AddGeneralsToList();
    }

    private void Update()
    {

    }

    void FindAllTroopTargets()
    {
        allTroopActors = FindObjectsOfType<TroopActor>().ToList();
    }

    void AddGeneralsToList()
    {
        m_troopControllerP1.m_generals.Clear();
        m_troopControllerP2.m_generals.Clear();

        foreach (TroopActor troop in allTroopActors)
        {
            if (troop.team == Team.TEAM1 && troop.rankState == TroopActor.RankState.IsGeneral)
            {
                m_troopControllerP1.AddGeneral(troop);
            }

            if (troop.team == Team.TEAM2 && troop.rankState == TroopActor.RankState.IsGeneral)
            {
                m_troopControllerP2.AddGeneral(troop);
            }
        }
    }
}
