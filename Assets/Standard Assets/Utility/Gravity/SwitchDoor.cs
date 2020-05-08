using UnityEngine;
using System.Collections;

public class SwitchDoor : MonoBehaviour, ITriggerSwitchFunction
{
    Transform m_MovingBegin;
    Transform m_MovingEnd;
    bool isMoving;
    float elapsedTime;
    Vector3 offset;
    public float m_wholeTime = 0.5f;
    // Use this for initialization
    void Start()
    {
        isMoving = false;
        m_MovingBegin = transform.Find("Begin");
        m_MovingEnd = transform.Find("End");
        if (m_wholeTime == 0)
        {
            m_wholeTime = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    StartTriggering();
        //}
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > m_wholeTime)
            {
                elapsedTime = m_wholeTime;
                isMoving = false;
            }
            print(m_MovingBegin);
            transform.position = offset + Vector3.Lerp(m_MovingBegin.position, m_MovingEnd.position, elapsedTime / m_wholeTime);
        }
    }

    public void StartTriggering()
    {
        isMoving = true;
        elapsedTime = 0f;
        offset = transform.position - m_MovingBegin.position;
    }
}
