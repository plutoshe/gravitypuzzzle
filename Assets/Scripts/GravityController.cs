using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_NormalGravityAccleration = 9.8f;
    private Rigidbody m_rigidbody;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    Vector3 UpdateByGravity(float i_deltaTime)
    {
        return i_deltaTime * new Vector3(0, -m_NormalGravityAccleration, 0);
    }

    private void Update()
    {
        m_rigidbody.AddForce(UpdateByGravity(Time.deltaTime) * m_rigidbody.mass);
    }
}
