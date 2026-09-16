using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseObjects;
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject settingsPanel;
    public void Pause()
    {
        pauseObjects.SetActive(true);
        buttonsPanel.SetActive(true);
        settingsPanel.SetActive(false);
        AudioManager.Instance.PauseGameMusic();
        Time.timeScale = 0;
    }

    public void Home()
    {
        AudioManager.Instance.PlayMenuMusic();
        Time.timeScale = 1;
        SceneManager.LoadScene("S_MainMenu");
    }

    public  void Resume()
    {
        pauseObjects.SetActive(false);
        AudioManager.Instance.ResumeGameMusic();
        Time.timeScale = 1;
    }


    public void OpenSettings()
    {
        buttonsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        buttonsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();

    }
    public void ClickButton()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClickSfx();
        }
    }
}
