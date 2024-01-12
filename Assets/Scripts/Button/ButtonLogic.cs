using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
  

    private void Start()
    {
        Cursor.visible = true;
        StartCoroutine(FadeInButton(startButton, 0.5f));
        StartCoroutine(FadeInButton(settingsButton, 1f));
        StartCoroutine(FadeInButton(quitButton, 1.5f));

    }
    private IEnumerator FadeInButton(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);
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
