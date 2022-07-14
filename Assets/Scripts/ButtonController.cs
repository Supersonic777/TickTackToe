using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private Design design;

    [Header("Game mode buttons: ")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject GamemodeMenu;
    [SerializeField] private GameObject OptionsMenu;

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
        MainMenu.SetActive(false);
        GamemodeMenu.SetActive(true);
    }

    public void OnOptionsButtonDown()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void OnPvpButtonDown()
    {
        SceneManager.LoadScene("PvP");
    }

    public void OnSingleplayerButtonDown()
    {
        SceneManager.LoadScene("Singleplayer");
    }

    public void OnBackButtonDown()
    {
        MainMenu.SetActive(true);
        GamemodeMenu.SetActive(false);
        OptionsMenu.SetActive(false);
    }


}
