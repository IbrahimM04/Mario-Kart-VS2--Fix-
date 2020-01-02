using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Driving : MonoBehaviour
{
    #region Debugging variables
    private TextMeshProUGUI tmpro;
    #endregion

    #region Visual variaibles
    private ParticleSystem[] boostFlames;
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

    #region drifting variables
    private int driftDirection;
    private bool canDrift;
    private bool canDriveDrift;
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
        canDrift = true;
        wheelRotationSpeed = 7;
        turnSpeed = 70;
        speed = 1200;
        maxSpeed = 2100;
        isBoosted = false;
        maxSpeedReverse = -75;
    }

    //working with FixedUpdate due to physics
    void FixedUpdate()
    {
        //Dis shit for debugging only
        tmpro.text = rb.velocity.magnitude.ToString();

        driving();
        if (canDrift)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
            {
                driftDirection = 0;
                drift(driftDirection);
            }
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
            {
                driftDirection = 1;
                drift(driftDirection);
            }
        }
    }

    private void driving()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotationDirection = new Vector3(0, -turnSpeed, 0);

            Quaternion deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationDirection = new Vector3(0, turnSpeed, 0);

            Quaternion deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
            print(deltaRotation);
        }


        if (Input.GetKey(KeyCode.W))
        {
            frontWheels[0].transform.Rotate(0, +wheelRotationSpeed, 0);
            backWheels.transform.Rotate(0, +wheelRotationSpeed, 0);

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
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * maxSpeedReverse);
            frontWheels[0].transform.Rotate(0, -wheelRotationSpeed / 3, 0);
            backWheels.transform.Rotate(0, -wheelRotationSpeed / 3, 0);
        }
    }

    private IEnumerator turningDrifting(int direction)
    {
        switch (direction)
        {
            case 0:
                rb.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
                for (int i = 0; i < 15; i++)
                {
                    rotationDirection = new Vector3(0, -turnSpeed, 0);

                    yield return new WaitForSeconds(0.00001f);

                    Quaternion deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
                    rb.MoveRotation(rb.rotation * deltaRotation);
                }
                rb.AddForce(new Vector3(0, -100, 0), ForceMode.Impulse);
                for (int i = 0; i < 15; i++)
                {
                    rotationDirection = new Vector3(0, -turnSpeed, 0);

                    yield return new WaitForSeconds(0.00001f);

                    Quaternion deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
                    rb.MoveRotation(rb.rotation * deltaRotation);
                }
                break;
            case 1:
                rb.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
                for (int i = 0; i < 15; i++)
                {
                    rotationDirection = new Vector3(0, turnSpeed, 0);

                    yield return new WaitForSeconds(0.00001f);

                    Quaternion deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
                    rb.MoveRotation(rb.rotation * deltaRotation);
                }
                rb.AddForce(new Vector3(0, -100, 0), ForceMode.Impulse);
                for (int i = 0; i < 15; i++)
                {
                    rotationDirection = new Vector3(0, -turnSpeed, 0);

                    yield return new WaitForSeconds(0.00001f);

                    Quaternion deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
                    rb.MoveRotation(rb.rotation * deltaRotation);
                }
                break;
        }
    }

    private void drift(int direction)
    {
        switch (direction)
        {
            case 0:


                break;

            case 1:


                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ram")
        {
            //ram shitz
        }
    }
}