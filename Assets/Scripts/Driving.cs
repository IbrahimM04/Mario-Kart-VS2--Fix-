﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Driving : MonoBehaviour
{
    private float timer;
    #region Debugging variables
    private TextMeshProUGUI tmpro;
    #endregion

    private Animator playerAnimator;

    #region Visual variables
    private ParticleSystem windTrails;
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
    [SerializeField] private float speed;
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
    private int driftTurnSpeed;
    private int driftDirection;
    private bool isDrifting;
    Quaternion deltaRotation;
    private float driftTimer;
    float speedBoostLevel;
    #endregion

    #region ramming
    private Vector3 ImpactAngle;
    #endregion

    //defining unity variables such as finding components of gameobjects
    private void Awake()
    {
        windTrails = GameObject.Find("Smoke Trails").GetComponent<ParticleSystem>();
        playerAnimator = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<Animator>();
        smoke = GameObject.FindGameObjectsWithTag("PSSmoke");
        rb = GetComponent<Rigidbody>();
        redBoostFlames = GameObject.FindGameObjectsWithTag("RedFlame");
        blueBoostFlames = GameObject.FindGameObjectsWithTag("BlueFlame");
    }

    //defining variables
    private void Start()
    {
        timer = 0;
        //drifter = driftingCoroutine(driftDirection);
        driftTimer = 0;
        firstDrift = true;
        isDrifting = false;
        wheelRotationSpeed = 7;
        turnSpeed = 60;
        speed = 800;
        maxSpeed = 2100;
        isBoosted = false;
        maxSpeedReverse = -200;

    }

    //working with FixedUpdate due to physics
    void FixedUpdate()
    {
        print(isBoosted);
        if(isBoosted == true)
        {
            speed = 2200;
            windTrails.Play();
        }
        else
        {
            speed = 800;
        }
        if (timer >= 1)
        {
            timer = 0;
            playerAnimator.SetInteger("AnimState", 0);
        }
        timer += Time.deltaTime;
        driftTimer += Time.fixedDeltaTime;

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
            driftDirection = 1;
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
            StartCoroutine(boost(speedBoostLevel));
            print(speedBoostLevel);
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
            playerAnimator.SetInteger("AnimState", 3);
            rotationDirection = new Vector3(0, -turnSpeed, 0);

            deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerAnimator.SetInteger("AnimState", 4);
            rotationDirection = new Vector3(0, turnSpeed, 0);

            deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        //key for forward
        if (Input.GetKey(KeyCode.W))
        {
            //frontWheels[0].transform.Rotate(+wheelRotationSpeed, 0, 0);
            //backWheels.transform.Rotate(+wheelRotationSpeed, 0, 0);

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
            //frontWheels[0].transform.Rotate(0, -wheelRotationSpeed / 3, 0);
            //backWheels.transform.Rotate(0, -wheelRotationSpeed / 3, 0);
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

        speedBoostLevel = 0;

        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].GetComponent<ParticleSystem>().Stop();
        }

        speedBoostLevel = 1;

        for (int i = 0; i < redBoostFlames.Length; i++)
        {
            redBoostFlames[i].GetComponent<SpriteRenderer>().enabled = true;
        }

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < redBoostFlames.Length; i++)
        {
            redBoostFlames[i].GetComponent<SpriteRenderer>().enabled = false;
        }

        speedBoostLevel = 2;

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
                    driftTurnSpeed = 45;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    driftTurnSpeed = 75;
                }
                else
                {
                    driftTurnSpeed = 60;
                }
                rotationDirection = new Vector3(0, -driftTurnSpeed, 0);

                deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
                //deltaRotation.z = 0;
                rb.MoveRotation(rb.rotation * deltaRotation);

                rb.AddRelativeForce(transform.forward.x + 1 * 400, 0, transform.forward.z + 1.3f * speed);
                break;
            case 1:
                if (Input.GetKey(KeyCode.A))
                {
                    driftTurnSpeed = 45;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    driftTurnSpeed = 75;
                }
                else
                {
                    driftTurnSpeed = 60;
                }
                rotationDirection = new Vector3(0, driftTurnSpeed, 0);

                deltaRotation = Quaternion.Euler(rotationDirection * Time.deltaTime);
                //deltaRotation.z = 0;
                rb.MoveRotation(rb.rotation * deltaRotation);

                rb.AddRelativeForce(transform.forward.x - 1 * 400, 0, transform.forward.z + 1.3f * speed);
                break;
        }
    }

    private IEnumerator boost(float seconds)
    {
        print(seconds);
        isBoosted = true;
        yield return new WaitForSeconds(seconds);
        isBoosted = false;
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
