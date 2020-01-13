using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }



    void Update()
    {


        transform.position = new Vector3(0, Mathf.Sin(Time.time * 3) / Mathf.PI / 7, 0);
    }
}
    
