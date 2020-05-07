using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public enum GravityDirection
{
    XPositive,
    XNegtive,
    YPositive,
    YNegtive,
    ZPositive,
    ZNegtive,
}

public class GravityDirectionRoot : MonoBehaviour
{
    public GravityDirection m_GravityDirection;
    // Start is called before the first frame update
    void Start()
    {
        UpdateRotationByGravityDirection(m_GravityDirection);
    }
    Vector3 UpdateRotationByGravityDirection(GravityDirection i_direction)
    {
        switch (i_direction)
        {
            case GravityDirection.XPositive: return new Vector3(1, 0, 0);
            case GravityDirection.XNegtive: return new Vector3(-1, 0, 0);
            case GravityDirection.YPositive: return new Vector3(0, 1, 0);
            case GravityDirection.YNegtive: return new Vector3(0, -1, 0);
            case GravityDirection.ZPositive: return new Vector3(0, 0, 1);
            case GravityDirection.ZNegtive: return new Vector3(0, 0, -1);
        }
        return new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    void ChangeGravityDirection(GravityDirection i_direction)
    {
        m_GravityDirection = i_direction;
        UpdateRotationByGravityDirection(m_GravityDirection);
    }
}
