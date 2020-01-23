using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int lapsFinished;

    #region Waypoint variables
    private RaycastHit hit;
    [SerializeField] private GameObject[] waypointRaycasts;
    [SerializeField] private LayerMask playerLayer;
    private int currentWaypoint;
    //last waypoint
    #endregion

    private TimeTracker timeTracker;

    private void Awake()
    {
        waypointRaycasts = GameObject.FindGameObjectsWithTag("Waypoint");
        //timeTracker = GameObject.Find("Waypoints").GetComponentInChildren<TimeTracker>();
    }

    private void Start()
    {
        currentWaypoint = 0;
    }

    void Update()
    {
        //Waypoint();
    }

    private void Waypoint()
    {
        if (currentWaypoint == waypointRaycasts.Length - 1)
        {
            if (Physics.Raycast(waypointRaycasts[currentWaypoint].gameObject.transform.position, waypointRaycasts[currentWaypoint].gameObject.transform.forward, out hit, 40f, playerLayer))
            {
                lapsFinished++;
                timeTracker.ResetTimer();
            }
        }
        else if(Physics.Raycast(waypointRaycasts[currentWaypoint].gameObject.transform.position, waypointRaycasts[currentWaypoint].gameObject.transform.forward, out hit, 40f, playerLayer))
        {
            currentWaypoint++;
        }
    }
}
