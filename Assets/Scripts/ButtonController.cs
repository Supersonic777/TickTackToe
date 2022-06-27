using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
   [Header("Game mode buttons: ")]
   [SerializeField] private GameObject singleplayerButton;
   [SerializeField] private GameObject pvpButton;
   [SerializeField] private GameObject optionsButton;
   [SerializeField] private GameObject exitButton;


    public void OnReplayButtonDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuButtonDown()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnPlayButtonDown()
    {
        gameObject.SetActive(false);
        singleplayerButton.SetActive(true);
        pvpButton.SetActive(true);
        exitButton.SetActive(false);
        optionsButton.SetActive(false);
    }

    public void OnPvpButtonDown()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnSingleplayerButtonDown()
    {
        SceneManager.LoadScene("Singleplayer");
    }
}
