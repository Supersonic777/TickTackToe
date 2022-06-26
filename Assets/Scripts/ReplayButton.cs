using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    public void OnButtonDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
