using UnityEngine;
using System.Collections;

public class RaisingCube : MonoBehaviour, IInteractionCube, ITriggerComparison
{
    private float m_followingOffset;
    private bool m_isFollowing;
    private GravityController m_player;
    public string m_triggerIdentifer;
    // Use this for initialization
    void Start()
    {
        m_isFollowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFollowing)
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            var point = m_player.m_Camera.ScreenToWorldPoint(new Vector3(x, y, 1.0f));
            transform.parent.position = m_player.m_Camera.transform.position + (point - m_player.m_Camera.transform.position).normalized * m_followingOffset;
        }
    }

    public void StartInteraction(GravityController i_player)
    {
        m_player = i_player;
        m_followingOffset = (transform.parent.position - m_player.m_Camera.transform.position).magnitude;
        
        m_isFollowing = true;
    }
    public void EndInteraction()
    {
        m_isFollowing = false;
    }

    public bool CompareIdentifier(string identifier)
    {
        return m_triggerIdentifer == identifier;
    }
}
