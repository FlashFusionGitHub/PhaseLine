using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangerSpawner : MonoBehaviour {

    [System.Serializable]
    public enum DoorState
    {
        closed,
        closing,
        open,
        opening
    }

    [System.Serializable]
    public struct HangerDoorData
    {
        public Transform m_hangerDoor;
        public Vector3 m_openPos;
        public Vector3 m_closedPos;

    }

    [Header("Spawn Stuff")]
    public GameObject tankPrefab;
    public Transform spawnPoint;
    public GameObject lastSpawnedObject;
    public float disBeforeNextSpawn;
    [SerializeField] bool spawnWaiting;

    [Header("Door Stuff")]
    [SerializeField] private HangerDoorData[] m_hangerDoors;
    [SerializeField] private DoorState doorState;
    float progress;
    [SerializeField] private float m_doorOpenTime;
    [SerializeField] private float m_doorCloseTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (spawnWaiting)
        {
            CheckSpawn();
        }
        if (safeToClose() && (doorState == DoorState.open))
        {
            StartClosingDoors();
        }

        if (doorState == DoorState.opening)
        {
            OpenDoors();
        }
        else if (doorState == DoorState.closing)
        {
            CloseDoors();
        }
        UpdateDoors();
    }

    //---------------------------------------------------------------------Door Voids-------------------------------------------------------

    void StartOpeningDoors()
    {
        doorState = DoorState.opening;
    }
    void StartClosingDoors()
    {
        doorState = DoorState.closing;
    }


    void OpenDoors()
    {
        if (progress < 1)
        {
            progress += Time.deltaTime / m_doorOpenTime;
        }
        else
        {
            progress = 1;
            doorState = DoorState.open;
        }
    }

    void CloseDoors()
    {
        if (progress > 0)
        {
            progress -= Time.deltaTime / m_doorCloseTime;
        }
        else
        {
            progress = 0;
            doorState = DoorState.closed;
        }
    }
    void UpdateDoors()
    {
        foreach (HangerDoorData hangerDoor in m_hangerDoors)
        { 
                hangerDoor.m_hangerDoor.localPosition = Vector3.Lerp(hangerDoor.m_closedPos, hangerDoor.m_openPos, progress);
        }
    }

    //------------------------------------------------------------------Spawn Voids-------------------------------------------------------------

    public void CheckSpawn()
    {
        if (doorState == DoorState.closed)
        {
            SpawnObject();
            spawnWaiting = false;
            StartOpeningDoors();
        }
        else
        {
            spawnWaiting = true;
        }
    }
    void SpawnObject()
    {
        lastSpawnedObject = Instantiate(tankPrefab, spawnPoint.position, transform.rotation);
    }
    bool safeToClose()
    {
        if (lastSpawnedObject)
        {
            if (Vector3.Distance(spawnPoint.position, lastSpawnedObject.transform.position) > disBeforeNextSpawn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPoint.position, disBeforeNextSpawn);
    }
}
