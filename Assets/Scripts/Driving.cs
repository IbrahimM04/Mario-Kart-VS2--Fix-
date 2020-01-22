using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Driving : MonoBehaviour
{
    #region Debugging variables
    private TextMeshProUGUI tmpro;
    #endregion

    #region Visual variables
    [SerializeField] private GameObject[] smoke;
    [SerializeField] private GameObject[] redBoostFlames;
    [SerializeField] private GameObject[] blueBoostFlames;
    [SerializeField] private float gravityScale;

    private bool smokeEnabled;
    private bool redBoostFlamesEnabled;
    private bool blueBoostFlamesEnabled;

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
    Quaternion deltaRotation;
    private float driftTimer;
    int speedBoostLevel;
    #endregion

    #region ramming
    private Vector3 ImpactAngle;
    #endregion

    //defining unity variables such as finding components of gameobjects
    private void Awake()
    {
        smoke = GameObject.FindGameObjectsWithTag("PSSmoke");
        rb = GetComponent<Rigidbody>();
        tmpro = GameObject.Find("SpeedText").GetComponent<TextMeshProUGUI>();
        redBoostFlames = GameObject.FindGameObjectsWithTag("RedFlame");
        blueBoostFlames = GameObject.FindGameObjectsWithTag("BlueFlame");
    }

    //defining variables
    private void Start()
    {
        //drifter = driftingCoroutine(driftDirection);
        driftTimer = 0;
        firstDrift = true;
        isDrifting = false;
        wheelRotationSpeed = 7;
        turnSpeed = 50;
        speed = 1200;
        maxSpeed = 2100;
        isBoosted = false;
        maxSpeedReverse = -75;

    }

    //working with FixedUpdate due to physics
    void FixedUpdate()
    {
        driftTimer += Time.fixedDeltaTime;

        //Dis shit for debugging only
        tmpro.text = rb.velocity.magnitude.ToString();

        if (!isDrifting)
        {
            driving();
        }
        else if (isDrifting)
        {
            print("working? Working!");
            drifting(driftDirection);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDrifting = true;
            driftTimer = 0;
            driftDirection = 0;
            StartCoroutine(driftingCoroutine(driftDirection));

            print("Start drifting");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDrifting = false;
            driftTimer = 0;
            StopCoroutine(driftingCoroutine(0));

            clearAllEffects();

            print("Stop drifting");
        }
        /*
        if (Input.GetKeyUp(KeyCode.LeftShift) && driftTimer >= 1f && driftTimer < 2f)
        {

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && driftTimer >= 2f)
        {

        }
        */

        rb.MovePosition(rb.position + new Vector3(0, gravityScale, 0));
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

    /// <summary>
    /// Drifting timer
    /// </summary>
    /// <param name="driftDir"></param>
    /// <returns></returns>
    private IEnumerator driftingCoroutine(int driftDir)
    {
        rb.AddForce(0, 6000, 0);

        yield return new WaitForSeconds(0.1f);

        rb.AddForce(0, -8000, 0);

        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].GetComponent<ParticleSystem>().Play();
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].GetComponent<ParticleSystem>().Stop();
        }

        for (int i = 0; i < redBoostFlames.Length; i++)
        {
            redBoostFlames[i].GetComponent<SpriteRenderer>().enabled = true;
        }

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < redBoostFlames.Length; i++)
        {
            redBoostFlames[i].GetComponent<SpriteRenderer>().enabled = false;
        }

        for (int i = 0; i < blueBoostFlames.Length; i++)
        {
            blueBoostFlames[i].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    //fucntion for drifting
    private void drifting(int driftDir)
    {
        switch (driftDir)
        {
            case 0:
                if (Input.GetKey(KeyCode.D))
                {
                    turnSpeed = 35;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    turnSpeed = 65;
                }
                else
                {
                    turnSpeed = 50;
                }
                rotationDirection = new Vector3(0, -turnSpeed, 0);

                deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
                //deltaRotation.z = 0;
                rb.MoveRotation(rb.rotation * deltaRotation);

                rb.AddRelativeForce(transform.forward.x + 1 * 1200, 0, transform.forward.z + 1 * speed);
                break;
            case 1:
                if (Input.GetKey(KeyCode.A))
                {
                    turnSpeed = 35;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    turnSpeed = 65;
                }
                else
                {
                    turnSpeed = 50;
                }
                rotationDirection = new Vector3(0, turnSpeed, 0);

                deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
                //deltaRotation.z = 0;
                rb.MoveRotation(rb.rotation * deltaRotation);

                rb.AddRelativeForce(transform.forward.x - 1 * 1200, 0, transform.forward.z + 1 * speed);
                break;
        }
    }

    private void clearAllEffects()
    {
        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].GetComponent<ParticleSystem>().Stop();
        }

        for (int i = 0; i < redBoostFlames.Length; i++)
        {
            redBoostFlames[i].GetComponent<SpriteRenderer>().enabled = false;
        }

        for (int i = 0; i < blueBoostFlames.Length; i++)
        {
            blueBoostFlames[i].GetComponent<SpriteRenderer>().enabled = false;
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
