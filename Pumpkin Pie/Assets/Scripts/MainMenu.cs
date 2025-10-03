using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayButton()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("House");
    }

    public void SettingsButton()
    {
        Debug.Log("Settings");
        gameObject.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
