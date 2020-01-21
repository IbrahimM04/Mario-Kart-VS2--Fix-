using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBoost : MonoBehaviour
{
    [SerializeField] private GameObject mushroom1;
    [SerializeField] private GameObject mushroom2;
    [SerializeField] private GameObject mushroom3;

   [SerializeField] private int itemCount; 
    // Start is called before the first frame update
    void Start()
    {
        itemCount = 3;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(itemCount > 0)
            {
                UseItem();
            }
            else if(itemCount == 0)
            {
                //nothing happens here
            }
        }
    }

    void UseItem()
    {
        itemCount -= 1;
        switch (itemCount)
        {
            case 2:
                Destroy(mushroom1);
                break;

            case 1:
                Destroy(mushroom2);
                break;

            case 0:
                Destroy(mushroom3);
                break;
        }
    }


}
