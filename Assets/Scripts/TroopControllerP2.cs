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
        if (FindObjectOfType<Controllers>().m_controller2.DPadLeft.WasPressed)
        {
            //if the tank index is less than or equal to zero
            if (index == 0)
            {
                //set the tank index to the current squad size
                index = m_generals.Count;
            }

            //destory the currect circle
            if (m_currentSelectionCircle != null)
                Destroy(m_currentSelectionCircle);

            index--;

            FindObjectOfType<CameraControllerP2>().MoveCameraTo(m_generals[index].transform.position.x, m_generals[index].transform.position.z - 10);

            m_currentSelectionCircle = Instantiate(m_selectionCircle, m_generals[index].transform.position, Quaternion.Euler(-90, 0, 0));
        }

        if (FindObjectOfType<Controllers>().m_controller2.DPadRight.WasPressed)
        {
            if (index == m_generals.Count - 1)
            {
                //set the tank index so the first element will have a selection ring
                index = -1;
            }

            //destory the currect circle
            if (m_currentSelectionCircle != null)
                Destroy(m_currentSelectionCircle);

            index++;

            FindObjectOfType<CameraControllerP2>().MoveCameraTo(m_generals[index].transform.position.x, m_generals[index].transform.position.z - 10);

            m_currentSelectionCircle = Instantiate(m_selectionCircle, m_generals[index].transform.position, Quaternion.Euler(-90, 0, 0));
        }

        if (FindObjectOfType<Controllers>().m_controller2.Action1.WasPressed && !FindObjectOfType<NavigationArrowP2>().airStrikeState)
        {
            m_generals[index].moveTarget.transform.position = FindObjectOfType<NavigationArrowP2>().currentMarker.transform.position;
        }

        if (m_currentSelectionCircle != null)
            m_currentSelectionCircle.transform.position = m_generals[index].transform.position;
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
