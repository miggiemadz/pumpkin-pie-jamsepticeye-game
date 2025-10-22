using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Misc")]
    [SerializeField] private GameInformation gameInformation;
    [SerializeField] private InputActionReference pauseAction;
    private bool gamePaused;
    private Camera camera;

    [Header("UI")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject previousMenu;
    [SerializeField] private GameObject checklist;
    [SerializeField] private GameObject dialogueBox;

    public GameObject PreviousMenu { get => previousMenu; set => previousMenu = value; }
    public GameObject DialogueBox { get => dialogueBox; set => dialogueBox = value; }
    public GameObject Checklist { get => checklist; set => checklist = value; }

    private void Awake()
    {
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
        gameInformation.CurrentCheckpoint = 0;
        gameInformation.CurrentQuests = 0;

        gameInformation.HasMilk = false;
        gameInformation.HasSugar = false;
        gameInformation.HasCinammon = false;
        gameInformation.HasPumpkin = false;
        gameInformation.HasEggs = false;

        Checklist.GetComponent<Checklist>().UpdateChecklist();
    }

    private void OnEnable()
    {
        pauseAction.action.Enable();
        SceneManager.sceneLoaded += OnSceneLoaded;

        pauseAction.action.performed += OnPause;
    }

    private void OnDisable()
    {
        pauseAction.action.performed -= OnPause;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            previousMenu = GameObject.Find("MainMenu");
        }
        else
        {
            previousMenu = pauseMenu;
            Checklist.SetActive(true);
        }

        camera = Camera.main;
        camera.clearFlags = CameraClearFlags.Depth;
        camera.backgroundColor = new Color(0f, 0f, 0f);
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        if (SceneManager.GetActiveScene().name != "MainMenu") 
        {
            gamePaused = !gamePaused;
            pauseMenu.SetActive(gamePaused);

            Time.timeScale = gamePaused ? 0f : 1f;
        }
    }

    public void SettingsButton()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void PlayButton()
    {
        gamePaused = !gamePaused;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    void Update()
    {

    }
}
