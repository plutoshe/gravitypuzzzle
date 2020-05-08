using UnityEngine;
using System.Collections;

public class SwitchTriggerWithPlayer : MonoBehaviour
{
    public GameObject m_Binding;
    public string m_CompareIdentifier;
    public float m_TriggerWaitTime;
    private ITriggerSwitchFunction switchTrigger;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator TriggerringStart()
    {

        yield return new WaitForSeconds(m_TriggerWaitTime);
        switchTrigger.StartTriggering();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<ITriggerComparison>() != null)
        {
            if (other.GetComponent<ITriggerComparison>().CompareIdentifier(m_CompareIdentifier))
            {
                switchTrigger = m_Binding.GetComponent<ITriggerSwitchFunction>();
                if (switchTrigger != null)
                {
                    StartCoroutine(TriggerringStart());
                }
            }
        }        
    }
}
