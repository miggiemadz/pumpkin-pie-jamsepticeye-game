#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private GameInformation gameInformation;

    [Header("Interact Action")]
    [SerializeField] private InputActionReference interact;
    [SerializeField] private SpriteRenderer interactPrompt;
    private bool canInteract;

    [Header("Trigger")]
    [SerializeField] private SphereCollider sphereCollider;

    [Header("Scene")]
#if UNITY_EDITOR
    [SerializeField] private SceneAsset scene;
#endif
    private string sceneName;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (scene != null)
        {
            sceneName = scene.name;
        }
#endif
    }

    private void OnEnable()
    {
        interact.action.Enable();
        interact.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        interact.action.performed -= OnInteract;
        interact.action.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (canInteract)
        {
            gameInformation.LastScene = sceneName;
            SceneManager.LoadScene(sceneName);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        canInteract = true;
        interactPrompt.gameObject.SetActive(other.CompareTag("Player"));
    }

    private void OnTriggerExit(Collider other)
    {
        canInteract = false;
        interactPrompt.gameObject.SetActive(!other.CompareTag("Player"));

    }
}
