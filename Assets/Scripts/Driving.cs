using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Driving : MonoBehaviour
{
    #region Debugging
    private TextMeshProUGUI tmpro;
    #endregion

    #region speed/Rigidbodies
    private Rigidbody rb;
    private Vector3 m_EulerAngleVelocity;
    private int speed;
    private int maxSpeed;
    private float generalTurnSpeed;
    private int turnSpeed;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tmpro = GameObject.Find("SpeedText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        turnSpeed = 20;
        speed = 20;
        maxSpeed = 45;
    }

    void FixedUpdate()
    {
      
        if (Input.GetKey(KeyCode.A))
        {
            m_EulerAngleVelocity = new Vector3(0, 25, 0);
            print(m_EulerAngleVelocity);

            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_EulerAngleVelocity = new Vector3(0, -25, 0);
            print(m_EulerAngleVelocity);

            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        tmpro.text = rb.velocity.magnitude.ToString();
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * speed);
            if(rb.velocity.magnitude >= maxSpeed)
            {
                rb.velocity *= 0.99f;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * -speed);
        }
    }
}
