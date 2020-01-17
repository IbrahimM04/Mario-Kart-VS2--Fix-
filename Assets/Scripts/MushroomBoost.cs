using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBoost : MonoBehaviour
{

    [SerializeField]private int itemCount; 
    // Start is called before the first frame update
    void Start()
    {
        
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

            }
        }
    }

    void UseItem()
    {
        itemCount -= 1;
    }
}
