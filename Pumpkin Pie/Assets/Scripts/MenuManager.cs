using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseAction;
    private bool gamePaused;

    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;


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
        gamePaused = !gamePaused;
        pauseMenu.SetActive(gamePaused);

        Time.timeScale = gamePaused ? 1f : 0f;
    }

    void Update()
    {
        
    }
}
