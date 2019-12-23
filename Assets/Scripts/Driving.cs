using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Driving : MonoBehaviour
{
    #region Debugging variables
    private TextMeshProUGUI tmpro;
    #endregion

    #region Speed/Rigidbodies variables
    private Rigidbody rb;
    private Vector3 rotationDirection;
    [SerializeField] private int speed;
    [SerializeField] private int maxSpeed;
    [SerializeField] private int turnSpeed;
    #endregion

    //defining unity variables such as finding components of gameobjects
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tmpro = GameObject.Find("SpeedText").GetComponent<TextMeshProUGUI>();
    }

    //defining variables
    private void Start()
    {
        turnSpeed = 60;
        speed = 200;
        maxSpeed = 2100;
    }

    //working with FixedUpdate due to physics
    void FixedUpdate()
    {
        #region move left & right
        if (Input.GetKey(KeyCode.A))
        {
            rotationDirection = new Vector3(0, -turnSpeed, 0);

            Quaternion deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
            print(deltaRotation);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationDirection = new Vector3(0, turnSpeed, 0);

            Quaternion deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
            print(deltaRotation);
        }
        #endregion

        //Dis shit for debugging only
        tmpro.text = rb.velocity.magnitude.ToString();

        #region move forward & backward
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * speed);

            //limits the speed of the kart
            if(rb.velocity.magnitude >= maxSpeed)
            {
                rb.velocity *= 0.99f;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * -speed);
        }
        #endregion
    }
}
