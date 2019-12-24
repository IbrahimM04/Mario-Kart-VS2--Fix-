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
    private float wheelRotationSpeed;
    private float rotationX;
    private Rigidbody rb;
    private Vector3 rotationDirection;
    [SerializeField] private int speed;
    [SerializeField] private int maxSpeed;
    [SerializeField] private int maxSpeedReverse;
    [SerializeField] private int boostSpeed;
    [SerializeField] private int turnSpeed;
    private bool isBoosted;
    [SerializeField] private GameObject[] frontWheels;
    [SerializeField] private Transform backWheels;
    #endregion

    #region garbage variables
    private bool firstTime;
    #endregion

    //defining unity variables such as finding components of gameobjects
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tmpro = GameObject.Find("SpeedText").GetComponent<TextMeshProUGUI>();
        backWheels = GameObject.Find("backWheels").GetComponentInChildren<Transform>();
    }

    //defining variables
    private void Start()
    {
        turnSpeed = 60;
        speed = 200;
        maxSpeed = 2100;
        isBoosted = false;
        maxSpeedReverse = -75;
        firstTime = true;
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
            if (firstTime)
            {
                wheelRotationSpeed = 50;
                firstTime = false;
            }
            frontWheels[0].transform.Rotate(0, wheelRotationSpeed, 0);
            backWheels.transform.Rotate(0, wheelRotationSpeed, 0);
            wheelRotationSpeed += 0.08f;

            rb.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * speed);

            //limits the speed of the kart
            if (rb.velocity.magnitude >= maxSpeed && isBoosted == false)
            {
                rb.velocity *= 0.95f;
            }
            else if (isBoosted && rb.velocity.magnitude >= boostSpeed)
            {
                rb.velocity *= 0.95f;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                firstTime = true;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (firstTime)
            {
                wheelRotationSpeed = -50;
                firstTime = false;
            }
            rb.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * maxSpeedReverse);
            frontWheels[0].transform.Rotate(0, wheelRotationSpeed, 0);
            backWheels.transform.Rotate(0, wheelRotationSpeed, 0);
            wheelRotationSpeed -= 0.08f;
            if (Input.GetKeyUp(KeyCode.S))
            {
                firstTime = true;
            }
        }
        #endregion
    }
}
