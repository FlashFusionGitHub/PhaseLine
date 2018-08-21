using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using InControl;

public class BomberSquadActor : MonoBehaviour {

    public List<GameObject> m_planes;

    public List<GameObject> m_bombs;

    public GameObject m_marker;

    public InputDevice m_controller;

    public bool m_beginStrike;

    public float timer = 5;

    public Team team;

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update() {

        if (team == Team.TEAM1)
            m_controller = InputManager.Devices[0];
        if (team == Team.TEAM2)
            m_controller = InputManager.Devices[1];

        if (!m_beginStrike)
        {
            if(team == Team.TEAM1)
            {
                transform.position += new Vector3(m_controller.LeftStickX, 0, m_controller.LeftStickY);
            }

            if (team == Team.TEAM2)
            {
                transform.position += new Vector3(-m_controller.LeftStickX, 0, -m_controller.LeftStickY);
            }

            if (m_controller.LeftBumper.IsPressed)
                transform.Rotate(Vector3.down * 30 * Time.deltaTime);
            if (m_controller.RightBumper.IsPressed)
                transform.Rotate(Vector3.up * 30 * Time.deltaTime);

            if (m_controller.Action1.WasPressed)
            {
                m_beginStrike = true;

                if (team == Team.TEAM1)
                    FindObjectOfType<UnitManagerP1>().m_airStrikeBegin = false;
                if (team == Team.TEAM2)
                    FindObjectOfType<UnitManagerP2>().m_airStrikeBegin = false;
            }
        }

        if (m_beginStrike)
        {
            //Start Planes Fight
            foreach (GameObject plane in m_planes)
                plane.transform.Translate(Vector3.forward * 50 * Time.deltaTime);

            Destroy(gameObject, 15);

            timer -= Time.deltaTime;

            //Drop bombs
            DropBombs();
        }
	}

    void DropBombs()
    {
        if(timer <= 0.0f)
        {
            foreach (GameObject bomb in m_bombs)
            {
                if (bomb != null)
                    bomb.SetActive(true);

                Destroy(bomb, 2);
            }
        }
    }
}
