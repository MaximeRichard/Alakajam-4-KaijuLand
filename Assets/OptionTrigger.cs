using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionTrigger : MonoBehaviour
{

    bool listening = false;
    bool settingsOpened = false;
    public GameObject optnPopup;
    public SettingsPanel settingsPanel;
    public EventSystem eventSyst;
    public PlayerManager pef;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            listening = true;
            optnPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            listening = false;
            optnPopup.SetActive(false);
        }
    }

    private void Awake()
    {
        settingsPanel.Init(eventSyst);
    }

    private void Update()
    {
        if (listening && !settingsOpened)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenSettings();
            }
        }

        if (settingsOpened)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseSettings();
            }
        }
    }

    public void CloseSettings()
    {
        pef.enabled = true;
        PlayerProfile.Instance.Save();
        settingsPanel.Hide();
    }

    public void OpenSettings()
    {
        pef.enabled = false;
        settingsPanel.Show();
    }
}
