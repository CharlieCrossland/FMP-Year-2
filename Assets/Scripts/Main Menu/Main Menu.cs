using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject mainMenu;

    private void Awake()
    {
        title.SetActive(true);
        mainMenu.SetActive(false);
    }

    private void Update()
    {
        WaitForInput();
    }

    void WaitForInput()
    {
        if (Input.anyKeyDown)
        {
            title.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        title.SetActive(false);
        mainMenu.SetActive(true);
    }
}
