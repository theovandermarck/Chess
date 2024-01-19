using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayButton : MonoBehaviour
{
    public string toBeLoaded;
    public GameObject MainScene;
    public GameObject GameSelect;
    public GameObject MultiplayerLobby;
    public void loadGameScene()
    {
        SceneManager.LoadScene(toBeLoaded);
    }
    public void loadMultiplayerMenu()
    {
        GameSelect.SetActive(false);
        MultiplayerLobby.SetActive(true);
    }
    public void loadGameSelect(bool Bool)
    {
        MainScene.SetActive(!Bool);
        GameSelect.SetActive(Bool);
    }
}