using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private GameObject[] waypointRaycasts;
    private int currentWaypoint;
    [SerializeField] private LayerMask playerLayer;

    private void Awake()
    {
        waypointRaycasts = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(waypointRaycasts[currentWaypoint].gameObject.transform.position, waypointRaycasts[currentWaypoint].gameObject.transform.forward, out hit, 40f, playerLayer))
        {
            print("Cholera");
        }
        Debug.DrawRay(waypointRaycasts[0].transform.position, waypointRaycasts[0].transform.forward);
    }
}
