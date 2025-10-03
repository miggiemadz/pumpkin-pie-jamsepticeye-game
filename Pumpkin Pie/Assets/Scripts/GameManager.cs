using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Misc")]
    [SerializeField] private GameInformation gameInformation;
    [SerializeField] private InputActionReference pauseAction;
    private bool gamePaused;

    [Header("Menus")]
    [SerializeField] private GameObject[] menus;

    [Header("Music")]
    [SerializeField] private AudioSource currentTheme;


    void Start()
    {
        
    }

    private void OnEnable()
    {
        pauseAction.action.Enable();
        pauseAction.action.performed += OnPause;
    }

    private void OnDisable()
    {
        pauseAction.action.performed -= OnPause;
        pauseAction.action.Disable();
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        if (SceneManager.GetActiveScene().name != "MainMenu") 
        {
            gamePaused = !gamePaused;
            menus[0].SetActive(gamePaused);

            Time.timeScale = gamePaused ? 0f : 1f;
        }
    }

    void Update()
    {
        currentTheme.volume = gameInformation.MusicVolume / 10;
    }
}
