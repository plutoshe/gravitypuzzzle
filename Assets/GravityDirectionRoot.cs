using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

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
    private GravityDirection m_gravityDirection;
    private Quaternion fromRotation, toRotation;
    private float ratio;

    public bool IsRotating = false;
    public float m_RotateSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
        UpdateRotationByGravityDirection(m_GravityDirection);
        ratio = 1;
        IsRotating = false;
        transform.rotation = toRotation;
    }
    void UpdateRotationByGravityDirection(GravityDirection i_direction)
    {
        ratio = 0;
        IsRotating = true;
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
    // Update is called once per frame
    void ChangeGravityDirection(GravityDirection i_direction)
    {
        m_GravityDirection = i_direction;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_GravityDirection++;
        }
        if (IsRotating)
        {
            ratio += Time.deltaTime * m_RotateSpeed;
            if (ratio > 1)
            {
                ratio = 1;
                IsRotating = false;
            }
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, ratio);   
        }
    }
}
