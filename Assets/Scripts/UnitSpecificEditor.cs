using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitActor)), CanEditMultipleObjects]
public class UnitSpecificEditor : Editor {

    public SerializedProperty tank_Prop, aaTank_Prop, helicopter_Prop, blimp_Prop;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
