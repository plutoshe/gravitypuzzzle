﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravityTrigger : MonoBehaviour
{
    public GravityDirection m_changedDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        var gravityRoot = other.GetComponentInParent<GravityDirectionRoot>();
        if (gravityRoot != null)
        {
            gravityRoot.m_CurrentLinkGravityInteraction = gameObject;
            gravityRoot.m_isReadyChangeDirection = true;
            gravityRoot.m_nextGravityDirection = m_changedDirection;
            if (gravityRoot.CompareTag("RotationCube"))
            {
                gravityRoot.m_isReadyChangeDirection = false;
                gravityRoot.m_GravityDirection = m_changedDirection;
                gravityRoot.StopRotation();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var gravityRoot = other.GetComponentInParent<GravityDirectionRoot>();
        if (gravityRoot != null && gravityRoot.m_CurrentLinkGravityInteraction == gameObject)
        {
            gravityRoot.m_isReadyChangeDirection = false;
            gravityRoot.m_nextGravityDirection = gravityRoot.m_GravityDirection;
        }
    }


}
