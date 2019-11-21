using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject PausedWindow;

    [SerializeField]
    GameObject OptionsWindow;

    [SerializeField]
    GameObject HelpWindow;

    [SerializeField]
    GameObject MenuUI;

    AudioManager audioManager;

    private enum MenuStates {Playing, Pause, Options, Help}
    MenuStates currentState;

    void Start()
    {
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape") && currentState == MenuStates.Pause)
        {
            currentState = MenuStates.Playing;
        }
        else if (Input.GetKeyDown("escape") && currentState == MenuStates.Playing)
        {
            currentState = MenuStates.Pause;
        }

        switch (currentState)
        {
            case MenuStates.Playing:
                currentState = MenuStates.Playing;
                PausedWindow.SetActive(false);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(false);
                Time.timeScale = 1;
                break;
            case MenuStates.Pause:
                PausedWindow.SetActive(true);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(true);
                Time.timeScale = 0;
                break;
            case MenuStates.Options:
                PausedWindow.SetActive(false);
                OptionsWindow.SetActive(true);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(true);
                Time.timeScale = 0;
                break;
            case MenuStates.Help:
                PausedWindow.SetActive(false);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(true);
                MenuUI.SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void DisplayOptions()
    {
        currentState = MenuStates.Options;
    }

    public void DisplayHelp()
    {
        currentState = MenuStates.Help;
    }

    public void Resume()
    {
        currentState = MenuStates.Playing;
    }

    public void Exit()
    {
        // Runtime code here
        #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();

        #endif
        // Runtime code here
    }

    public void BackButton()
    {
        currentState = MenuStates.Pause;
    }

    public void SetSFXVolume(float sfxLv)
    {
        audioManager.SetSFXVolume(sfxLv);
    }

    public void SetMusicVolume(float musicLv)
    {
        audioManager.SetMusicVolume(musicLv);
    }

    public void SetMasterVolume(float masterLv)
    {
        audioManager.SetMasterVolume(masterLv);
    }
}
