﻿using UnityEngine;
using System.Collections;

public class cubePhysicsController : MonoBehaviour
{
    LayerMask m_CollisionMask;
    public float m_NormalGravityAccleration = 9.8f;
    private Rigidbody m_rigidbody;
    private GravityDirectionRoot m_rotationRoot;
    private Transform downDetection;
    // Use this for initialization
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rotationRoot = transform.parent.GetComponent<GravityDirectionRoot>();
        downDetection = transform.Find("Down");
        m_CollisionMask = LayerMask.GetMask("Level") | LayerMask.GetMask("Default");
    }
    private bool isGrounded()
    {
        RaycastHit hit;
        var direction = downDetection.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, direction.magnitude + 10, m_CollisionMask))
        {
            if (hit.distance < direction.magnitude + 0.1f)
            {
                return true;
            }
        }
        return false;
    }
    private void UpdateCharacterPosition(Vector3 direction, Vector3 startPosition, float innerDist)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPosition, direction, out hit, direction.magnitude + innerDist + 1, m_CollisionMask))
        {
            if (hit.distance < direction.magnitude + innerDist)
            {
                transform.parent.position += direction.normalized * (hit.distance - innerDist);
            }
            else
            {
                transform.parent.position += direction;
            }
        }
        else
        {
            transform.parent.position += direction;
        }
    }
    Vector3 UpdateByGravity(float i_deltaTime)
    {
        return i_deltaTime * transform.up * -m_NormalGravityAccleration;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_rotationRoot == null || !m_rotationRoot.IsRotating)
        {

            if (!isGrounded())
            {
                UpdateCharacterPosition(UpdateByGravity(Time.deltaTime) * m_rigidbody.mass, transform.position, (downDetection.position - transform.position).magnitude);
            }
        }
    }
}
