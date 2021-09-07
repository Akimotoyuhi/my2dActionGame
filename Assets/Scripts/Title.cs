using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private GameObject m_title;
    [SerializeField] private GameObject m_select;
    private bool m_swicth = false;

    private void Start()
    {
        m_select.SetActive(false);
    }

    private void Update()
    {
        if (!m_swicth && Input.anyKeyDown)
        {
            TitleButton();
        }
    }

    public void TitleButton()
    {
        m_swicth = true;
        m_title.SetActive(false);
        m_select.SetActive(true);
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
