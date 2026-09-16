using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMenuMusic();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("S_GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
