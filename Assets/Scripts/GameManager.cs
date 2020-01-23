using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int lapsFinished;

    #region Waypoint variables
    private RaycastHit hit;
    int fuckyou;
    [SerializeField] private GameObject[] waypointRaycasts;
    [SerializeField] private LayerMask playerLayer;
    private int currentWaypoint;

    bool yeet;
    bool kek;
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
        kek = true;
        currentWaypoint = 0;
        fuckyou = 0;
    }

    void Update()
    {
        if (fuckyou >= 2)
        {
            if(kek == true)
            {
                print("yes to kek");
                kek = false;
                timeTracker.ResetTimer();
            }
            
        }
        //Waypoint();
        CheckLastWaypoint();
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

    private void CheckLastWaypoint()
    {
        if(Physics.Raycast(waypointRaycasts[currentWaypoint].gameObject.transform.position, waypointRaycasts[currentWaypoint].gameObject.transform.forward, out hit, 40f, playerLayer))
        {
            if(yeet == true)
            {
                print("yes to yeet");
                fuckyou++;
                StartCoroutine(delay());
            }
        }
    }

    private IEnumerator delay()
    {
        yeet = false;
        yield return new WaitForSeconds(1);
        yeet = true;
    }
}
