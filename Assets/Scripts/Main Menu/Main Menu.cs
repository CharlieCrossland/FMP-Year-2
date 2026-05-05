using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Animator animator;


    private void Update()
    {
        WaitForInput();
    }

    void WaitForInput()
    {
        if (Input.anyKeyDown)
        {
            animator.SetTrigger("titleInput");
        }
    }

    public void Play()
    {
        StartCoroutine(playAnimation());
    }

    private IEnumerator playAnimation()
    {
        animator.SetTrigger("play");
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene("Game");
        yield break;
    }

    public void Exit()
    {
        StartCoroutine(exitAnimation());
    }

    private IEnumerator exitAnimation()
    {
        animator.SetTrigger("exit");
        yield return new WaitForSeconds(2.2f);
        Application.Quit();
        yield break;
    }
}
