using UnityEngine;

/// <summary>
/// Simple pause menu controller that forwards to the settings menu or quits the application.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu; // Reference to the settings menu to open

    void Start()
    {
        // Reserved for initialization if needed in the future
    }

    void Update()
    {
        // Intentionally blank
    }

    /// <summary>
    /// Show the settings menu and hide the pause menu.
    /// </summary>
    public void SettingsButton()
    {
        gameObject.SetActive(false);
        settingsMenu.SetActive(true);
    }

    /// <summary>
    /// Quit the application. Works only in build; no-op in the editor.
    /// </summary>
    public void QuitButton()
    {
        Application.Quit();
    }

}
