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
        m_controller = InputManager.Devices[0];

        if(m_controller.LeftBumper.IsPressed)
        {
            transform.Rotate(Vector3.up * 30 * Time.deltaTime);
        }

        if (m_controller.RightBumper.IsPressed)
        {
            transform.Rotate(-Vector3.up * 30 * Time.deltaTime);
        }

        transform.position += new Vector3(m_controller.LeftStickX, 0, m_controller.LeftStickY);

        if (m_controller.Action1.WasPressed)
        {
            m_beginStrike = true;
        }

        if (m_beginStrike)
        {
        //Start Planes Fight
        foreach (GameObject plane in m_planes)
            plane.transform.Translate(Vector3.forward * 50 * Time.deltaTime);

            //Destory arrow
            Destroy(m_marker);
            //Drop bombs

            //Destory BomberSquad
            Destroy(this.gameObject, 15);
        }
	}
}
