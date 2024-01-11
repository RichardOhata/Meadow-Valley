using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
  

    private void Start()
    {
        StartCoroutine(FadeInButton(startButton, 0f));
        StartCoroutine(FadeInButton(settingsButton, 0.5f));
        StartCoroutine(FadeInButton(quitButton, 1f));

    }
    private IEnumerator FadeInButton(Button button, float delay)
    {

        yield return new WaitForSeconds(delay);
        Debug.Log(button.gameObject.name);
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        while (canvasGroup.alpha <= 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
