using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityStandardAssets.Characters.FirstPerson;

public enum GravityDirection
{
    XPositive = 0,
    XNegtive,
    YPositive,
    YNegtive,
    ZPositive,
    ZNegtive,
}

public class GravityDirectionRoot : MonoBehaviour
{
    
    public GravityDirection m_GravityDirection
    {
        get
        {
            return m_gravityDirection;
        }
        set
        {
            if (!IsRotating)
            {
                m_gravityDirection = value;
                UpdateRotationByGravityDirection(m_gravityDirection);
            }
        }
    }
    [SerializeField]
    public GravityDirection m_gravityDirection;
    public GravityDirection m_nextGravityDirection;
    public GameObject m_CurrentLinkGravityInteraction;
    private Quaternion fromRotation, toRotation;
    public bool m_isReadyChangeDirection;
    private float ratio;

    public bool IsRotating = false;
    public float m_RotateSpeed = 1f;

    public void StopRotation()
    {
        ratio = 1;
        IsRotating = false;
        transform.rotation = toRotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        UpdateRotationByGravityDirection(m_GravityDirection);
        StopRotation();
    }
    void GetRotationReady(GravityDirection i_direction)
    {
        switch (i_direction)
        {
            case GravityDirection.XPositive: fromRotation = transform.rotation; toRotation = Quaternion.Euler(-90, 0, 90); break;
            case GravityDirection.XNegtive: fromRotation = transform.rotation; toRotation = Quaternion.Euler(-90, 0, -90); break;
            case GravityDirection.YPositive: fromRotation = transform.rotation; toRotation = Quaternion.Euler(180, 0, 0); break;
            case GravityDirection.YNegtive: fromRotation = transform.rotation; toRotation = Quaternion.Euler(0, 0, 0); break;
            case GravityDirection.ZNegtive: fromRotation = transform.rotation; toRotation = Quaternion.Euler(90, 0, 0); break;
            case GravityDirection.ZPositive: fromRotation = transform.rotation; toRotation = Quaternion.Euler(-90, 0, 0); break;
        }
    }

    void UpdateRotationByGravityDirection(GravityDirection i_direction)
    {
        ratio = 0;
        IsRotating = true;
        GetRotationReady(i_direction);
    }
    // Update is called once per frame
    void ChangeGravityDirection(GravityDirection i_direction)
    {
        m_GravityDirection = i_direction;
    }

    private void Update()
    {
        //transform.RotateAround(transform.position, Vector3.forward, 30 * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Z) && m_isReadyChangeDirection)
        {
            m_isReadyChangeDirection = false;
            m_GravityDirection = m_nextGravityDirection;
        }
        if (IsRotating)
        {
            ratio += Time.deltaTime * m_RotateSpeed;
            if (ratio > 1)
            {
                ratio = 1;
                IsRotating = false;
            }
            print(toRotation + "," + ratio);
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, ratio);   
        }
    }
}
