using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite m_EmptyFocusImg;
    public Sprite m_FocusImg;
    private Image m_currentFocusImage;

    void Start()
    {
        m_currentFocusImage = GetComponent<Image>();
        m_currentFocusImage.sprite = m_EmptyFocusImg;
    }

    public void OnFocus()
    {
        m_currentFocusImage.sprite = m_FocusImg;
    }

    public void OnLeaveFocus()
    {
        m_currentFocusImage.sprite = m_EmptyFocusImg;
    }
}
