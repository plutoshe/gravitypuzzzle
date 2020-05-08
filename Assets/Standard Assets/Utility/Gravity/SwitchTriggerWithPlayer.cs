using UnityEngine;
using System.Collections;

public class SwitchTriggerWithPlayer : MonoBehaviour
{
    ITriggerSwitchFunction m_Binding;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Binding.StartTriggering();
        }        
    }
}
