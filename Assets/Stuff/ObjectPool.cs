using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {


    [SerializeField] public TroopActor[] allTroopActors;

    // Use this for initialization
    void Start () {
        FindAllTroopTargets();
	}
	
    void FindAllTroopTargets()
    {
        allTroopActors = FindObjectsOfType<TroopActor>();
    }
}
