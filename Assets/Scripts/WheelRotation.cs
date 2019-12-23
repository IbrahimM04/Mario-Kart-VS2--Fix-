using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    private bool canTurn = false;

    private int rotationSpeed;

    private void Awake()
    {

    }

    void Start()
    {
        if (gameObject.name == "frontwheel")
        {
            canTurn = true;
        }
        else
        {
            canTurn = false;
        }
    }
    
    void Update()
    {
        
    }

    public void updateTurningState(int state)
    {
        if (canTurn)
        {
            switch (state)
            {
                case -1:
                    //Chagne front wheel lcoations

                    break;
                case 0:
                    //Chagne front wheel lcoations

                    break;
                case 1:
                    //Chagne front wheel lcoations

                    break;
            }
        }
    }
}
