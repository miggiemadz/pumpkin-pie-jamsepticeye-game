using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance for global access.
    /// </summary>
    public static GameManager Instance;

    [Header("Misc")]
    [SerializeField]
    private GameInformation gameInformation; // Reference to shared game state (ScriptableObject)
    [SerializeField]
    private InputActionReference pauseAction; // Input action used to toggle the pause menu
    private bool gamePaused; // Tracks whether the game is currently paused
    private Camera camera; // Cached main camera reference

    [Header("UI")]
    [SerializeField]
    private GameObject pauseMenu; // Pause menu root GameObject
    [SerializeField]
    private GameObject settingsMenu; // Settings menu root GameObject
    [SerializeField]
    private GameObject previousMenu; // Reference to the previously active menu
    [SerializeField]
    private GameObject checklist; // Reference to the in-game checklist UI
    [SerializeField]
    private GameObject dialogueBox; // Reference to the global dialogue UI

    public GameObject PreviousMenu { get => previousMenu; set => previousMenu = value; }
    public GameObject DialogueBox { get => dialogueBox; set => dialogueBox = value; }
    public GameObject Checklist { get => checklist; set => checklist = value; }

    private void Awake()
    {
        // Simple singleton pattern: ensure only one GameManager persists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Initialize game state defaults
        gameInformation.CurrentCheckpoint = 0;
        gameInformation.CurrentQuests = 0;

        gameInformation.InitialMonologe = false;

        gameInformation.HasMilk = false;
        gameInformation.HasSugar = false;
        gameInformation.HasCinammon = false;
        gameInformation.HasPumpkin = false;
        gameInformation.HasEggs = false;

        // Ensure checklist UI reflects initial state
        Checklist.GetComponent<Checklist>().UpdateChecklist();
    }

    private void OnEnable()
    {
        // Subscribe to pause input and Unity sceneLoaded event
        pauseAction.action.Enable();
        SceneManager.sceneLoaded += OnSceneLoaded;

        pauseAction.action.performed += OnPause;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        pauseAction.action.performed -= OnPause;
    }

    /// <summary>
    /// Called when a new scene is loaded. Adjusts UI references and camera defaults.
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            // In the main menu, set previousMenu to the main menu object
            previousMenu = GameObject.Find("MainMenu");
        }
        else
        {
            // For gameplay scenes, use the pauseMenu as the previous menu and enable checklist
            previousMenu = pauseMenu;
            Checklist.SetActive(true);
        }

        camera = Camera.main;
        camera.clearFlags = CameraClearFlags.Depth;
        camera.backgroundColor = new Color(0f, 0f, 0f);
    }

    /// <summary>
    /// Toggles the pause state when the pause action is performed. Pausing is disabled on the MainMenu scene.
    /// </summary>
    private void OnPause(InputAction.CallbackContext ctx)
    {
        if (SceneManager.GetActiveScene().name != "MainMenu") 
        {
            gamePaused = !gamePaused;
            pauseMenu.SetActive(gamePaused);

            Time.timeScale = gamePaused ? 0f : 1f;
        }
    }

    /// <summary>
    /// Show the settings panel from the pause menu.
    /// </summary>
    public void SettingsButton()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    /// <summary>
    /// Resume gameplay from the pause menu.
    /// </summary>
    public void PlayButton()
    {
        gamePaused = !gamePaused;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Quit the application (works in builds; no-op in the editor).
    /// </summary>
    public void QuitButton()
    {
        Application.Quit();
    }

    void Update()
    {
        // Intentionally blank (reserved for future manager updates)
    }
}
