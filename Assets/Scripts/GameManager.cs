using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Waypoint variables
    private RaycastHit hit;
    [SerializeField] private GameObject[] waypointRaycasts;
    [SerializeField] private LayerMask playerLayer;
    private int waypointsPassed;
    private int currentWaypoint;
    #endregion

    private void Awake()
    {
        waypointRaycasts = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    private void Start()
    {
        currentWaypoint = 0;
        waypointsPassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Waypoint();
    }

    private void Waypoint()
    {
        if (Physics.Raycast(waypointRaycasts[currentWaypoint].gameObject.transform.position, waypointRaycasts[currentWaypoint].gameObject.transform.forward, out hit, 40f, playerLayer))
        {
            currentWaypoint++;
            waypointsPassed++;
            print("Cholera");
            if(waypointsPassed >= waypointRaycasts.Length)
            {
                
            }
        }
        Debug.DrawRay(waypointRaycasts[0].transform.position, waypointRaycasts[0].transform.forward);
    }
}
