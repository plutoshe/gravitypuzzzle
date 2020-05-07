using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_NormalGravityAccleration = 9.8f;
    public float m_UnitMovement = 1.0f;
    private Rigidbody m_rigidbody;
    private GravityDirectionRoot m_rotationRoot;
    private float camPitch = 0;
    public float m_SpeedH = 2;
    private float camYaw = 0;
    public float m_SpeedV = 2;
    public Camera m_Camera;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rotationRoot = transform.parent.GetComponent<GravityDirectionRoot>();
        m_Camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    Vector3 UpdateByGravity(float i_deltaTime)
    {
        return i_deltaTime * new Vector3(0, -m_NormalGravityAccleration, 0);
    }

    private Vector3 TackleInput()
    {
        Vector3 movement = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            movement.z += m_UnitMovement;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.z -= m_UnitMovement;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= m_UnitMovement;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += m_UnitMovement;
        }
        return movement;
    }

    private void Update()
    {

        if (m_rotationRoot == null || !m_rotationRoot.IsRotating)
        {
            m_rigidbody.AddRelativeForce(UpdateByGravity(Time.deltaTime) * m_rigidbody.mass);
            transform.localPosition += TackleInput() * Time.deltaTime;
            camYaw += m_SpeedH * Input.GetAxis("Mouse X");
            camPitch -= m_SpeedV * Input.GetAxis("Mouse Y");
            camPitch = Mathf.Clamp(camPitch, -90, 90);

            if (m_Camera)
            {
                print(camYaw);
                m_Camera.transform.localEulerAngles = new Vector3(camPitch, camYaw, 0);
            }
        }
        
    }
}
