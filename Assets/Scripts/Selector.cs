using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    private int index;

    private Transform[] heartPositions;

    private GameObject playerHeart;

    private void Awake()
    {
        playerHeart = GameObject.Find("Insert player anme here");
    }

    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && index >= 1)
            {
                index--;
                playerHeart.transform.position = heartPositions[index].transform.position;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && index <= 2)
            {
                index++;
                playerHeart.transform.position = heartPositions[index].transform.position;
            }
        }
    }
}
