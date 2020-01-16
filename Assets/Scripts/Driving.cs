using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Driving : MonoBehaviour
{
    #region Debugging variables
    private TextMeshProUGUI tmpro;
    public int yeet;
    public int kek;
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
    private bool firstDrift;
    #endregion

    #region drifting variables
    private int driftDirection;
    private bool isDrifting;
    private bool canDriveDrift;
    Quaternion deltaRotation;
    private float driftTimer;
    #endregion

    #region ramming
    private Vector3 ImpactAngle;
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
        driftTimer = 0;
        firstDrift = true;
        isDrifting = false;
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
        driftTimer += Time.deltaTime;
        //Dis shit for debugging only
        tmpro.text = rb.velocity.magnitude.ToString();

        if (!isDrifting)
        {
            driving();
        }
        else if (isDrifting)
        {
            drifting(driftDirection);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            driftTimer = 0;
            driftDirection = 1;
            isDrifting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDrifting = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && driftTimer >= 1f && driftTimer < 2f)
        {

        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) && driftTimer >= 2f)
        {

        }
    }

    private void driving()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotationDirection = new Vector3(0, -turnSpeed, 0);

            deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationDirection = new Vector3(0, turnSpeed, 0);

            deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        //key for forward
        if (Input.GetKey(KeyCode.W))
        {
            frontWheels[0].transform.Rotate(0, +wheelRotationSpeed, 0);
            backWheels.transform.Rotate(0, +wheelRotationSpeed, 0);

            rb.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * speed);

            //limits the speed of the kart
            if (isBoosted == false && rb.velocity.magnitude >= maxSpeed)
            {
                rb.velocity *= 0.95f;
            }
            else if (isBoosted == true && rb.velocity.magnitude >= boostSpeed)
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

    //fucntion for drifting
    private void drifting(int driftDir)
    {
        switch (driftDir)
        {
            case 0:
                rotationDirection = new Vector3(0, -turnSpeed / 5, 0);

                deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);

                rb.AddForce(new Vector3(transform.forward.x - 1, 0, transform.forward.z + 1) * speed / 2.1f);
                break;
            case 1:
                rotationDirection = new Vector3(0, -turnSpeed / 5, 0);

                deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
                
                rb.AddForce(new Vector3(transform.forward.x + 1, 0, transform.forward.z + 1) * speed / 2.1f);
                break;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ram")
        {
            //ram shit
        }
    }
}