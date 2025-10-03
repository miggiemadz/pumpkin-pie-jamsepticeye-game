using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void SettingsButton()
    {
        gameObject.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
