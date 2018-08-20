using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using InControl;

public class BomberSquadActor : MonoBehaviour {

    public List<GameObject> m_planes;
    public GameObject m_marker;
    InputDevice m_controller;

    bool m_beginStrike;

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update() {
        //m_controller = InputManager.Devices[0];

        //gameObject.transform.rotation = Quaternion.Euler(-m_controller.LeftBumper, 0, 0);
        //gameObject.transform.rotation = Quaternion.Euler(m_controller.LeftBumper, 0, 0);

        //gameObject.transform.position = new Vector3(m_controller.LeftStickX, m_controller.LeftStickY, 0);

        //if(m_controller.Action1.WasPressed)
        //{
        //    m_beginStrike = true;
        //}

        //if (m_beginStrike)
        //{
        //Start Planes Fight
        foreach (GameObject plane in m_planes)
                plane.transform.Translate(Vector3.forward * 50 * Time.deltaTime);

            //Destory arrow
            Destroy(m_marker);
            //Drop bombs

            //Destory BomberSquad
        //}

        Destroy(this.gameObject, 15);
	}
}
