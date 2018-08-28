using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopControllerP2 : MonoBehaviour {

    public List<TroopActor> m_generals = new List<TroopActor>();
    int index = 0;

    [Header("Selection Circle")]
    [SerializeField]
    private GameObject m_selectionCircle;
    private GameObject m_currentSelectionCircle;

    List<CaptureZoneActor> zonesCaptured;

    // Use this for initialization
    void Start()
    {
        m_currentSelectionCircle = Instantiate(m_selectionCircle, m_generals[0].transform.position, Quaternion.Euler(-90, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && m_generals.Count > 0)
        {
            foreach(TroopActor gen in m_generals.ToArray())
            {
                gen.Die(gen);
            }

            Debug.Log(m_generals.Count);
        }

        if (m_generals.Count == 0)
            Destroy(m_currentSelectionCircle);

        if (FindObjectOfType<Controllers>().m_controller2.DPadLeft.WasPressed && m_generals.Count > 1)
        {
            //destory the currect circle
            if (m_currentSelectionCircle != null)
                Destroy(m_currentSelectionCircle);

            CheckGeneralState(false, true);
        }

        if (FindObjectOfType<Controllers>().m_controller2.DPadRight.WasPressed && m_generals.Count > 1)
        {
            //destory the currect circle
            if (m_currentSelectionCircle != null)
                Destroy(m_currentSelectionCircle);

            CheckGeneralState(true, false);
        }

        if (FindObjectOfType<Controllers>().m_controller2.Action1.WasPressed && !FindObjectOfType<NavigationArrowP2>().airStrikeState)
        {
            m_generals[index].moveTarget.transform.position = FindObjectOfType<NavigationArrowP2>().currentMarker.transform.position;
        }

        if (m_currentSelectionCircle != null && index >= 0 && m_generals.Count > 0)
            m_currentSelectionCircle.transform.position = m_generals[index].transform.position;
    }

    void CheckGeneralState(bool increase, bool decrease)
    {
        if (increase)
        {
            index++;

            if (index >= m_generals.Count)
                index = 0;

            FindObjectOfType<CameraControllerP2>().MoveCameraTo(m_generals[index].transform.position.x, m_generals[index].transform.position.z - 10);

            m_currentSelectionCircle = Instantiate(m_selectionCircle, m_generals[index].transform.position, Quaternion.Euler(-90, 0, 0));
        }
        if (decrease)
        {
            if (index <= 0)
                index = m_generals.Count;

            index--;

            FindObjectOfType<CameraControllerP2>().MoveCameraTo(m_generals[index].transform.position.x, m_generals[index].transform.position.z - 10);

            m_currentSelectionCircle = Instantiate(m_selectionCircle, m_generals[index].transform.position, Quaternion.Euler(-90, 0, 0));
        }
    }

    public void AddGeneral(TroopActor troop)
    {
        m_generals.Add(troop);
    }

    public void RemoveGenereal(TroopActor troop)
    {
        m_generals.Remove(troop);
    }
}
