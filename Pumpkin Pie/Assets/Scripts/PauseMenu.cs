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
