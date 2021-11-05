using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour {

    private enum CurrentScreen {Instructions, Controls, Credits, Pause, None};
    private CurrentScreen currentScreen;

    public PlayerProgression PlayerProgress;
    public GameObject optionsScreen;
    public GameObject controlsScreen;
    public GameObject audioOptionsScreen;
    public GameObject mainScreen;
    public GameObject lvlSelScreen;

    // Use this for initialization
    void Start () {
        currentScreen = CurrentScreen.None;
        OnBackClick();
	}

    public void OnOptionsClick()
    {
        optionsScreen.SetActive(true);
        mainScreen.SetActive(false);
    }

    public void OnControlsClick()
    {
        controlsScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void OnAudioOptionsClick()
    {
        audioOptionsScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void OnNewGameClick()
    {
        PlayerProgress.LevelsCompleted = 0;
        PlayerProgress.CurrentCheckPoint = 0;
        PlayerProgress.CurrentLevel = 1;
        SceneManager.LoadScene(1);
    }

    public void OnContinueClick()
    {
        SceneManager.LoadScene(PlayerProgress.CurrentLevel);
        //TODO load save file and load appropriate scene
    }

    public void OnCreditsClick()
    {
        //TODO load credits scene
    }

    public void OnLevelSelectClick()
    {
        mainScreen.SetActive(false);
        lvlSelScreen.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown("backspace"))
        {
            OnBackClick();
        }
    }

    public void OnBackClick()
    {
        mainScreen.SetActive(true);
        controlsScreen.SetActive(false);
        optionsScreen.SetActive(false);
        audioOptionsScreen.SetActive(false);
        lvlSelScreen.SetActive(false);
    }

    public void OnExitClick()
    {
        SceneManager.LoadScene("Credits");
    }
}
