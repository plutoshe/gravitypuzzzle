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
    private GameObject m_Help;
    private Text m_HelpText;
    void Start()
    {
        m_currentFocusImage = GetComponent<Image>();
        m_currentFocusImage.sprite = m_EmptyFocusImg;
        m_Help = transform.Find("Help").gameObject;
        m_HelpText = m_Help.GetComponentInChildren<Text>();
        m_Help.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            m_Help.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowHelp("123\n");
        }
    }

    public void OnFocus()
    {
        m_currentFocusImage.sprite = m_FocusImg;
    }

    public void OnLeaveFocus()
    {
        m_currentFocusImage.sprite = m_EmptyFocusImg;
    }
    IEnumerator AutomationCloseHelp()
    {
        yield return new WaitForSeconds(3.5f);
        m_Help.SetActive(false);
    }
    public void ShowHelp(string helpText)
    {
        m_Help.SetActive(true);
        m_HelpText.text = helpText + "\nUse H to hide window";
        StartCoroutine(AutomationCloseHelp());
    }
}
