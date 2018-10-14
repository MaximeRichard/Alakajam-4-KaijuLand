using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance = null;

    public GameObject pausePanel,deathPanel;

    private bool paused;
    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        paused = false;
        pausePanel.SetActive(false);
        deathPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)){
            SwitchPause(!paused);
        }
        if (Input.GetKeyDown(KeyCode.R) && deathPanel.activeSelf)
        {
            ReloadScene();
        }
    }

    private void SwitchPause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
        paused = isPaused;
    }

    public void ClickContinue()
    {
        SwitchPause(false);
    }

    public void ClickBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnLevelWasLoaded(int level)
    {
        paused = false;
        pausePanel.SetActive(false);
        deathPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
