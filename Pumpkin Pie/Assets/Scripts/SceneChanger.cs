#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Misc")]
    [SerializeField] private GameInformation gameInformation;
    [SerializeField] private SpriteRenderer interactPrompt;

    [Header("Scene")]
#if UNITY_EDITOR
    [SerializeField] private SceneAsset scene;
#endif
    private string sceneName;

    public SpriteRenderer InteractPrompt { get => interactPrompt; set => interactPrompt = value; }
    public string SceneName { get => sceneName; set => sceneName = value; }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (scene != null)
        {
            SceneName = scene.name;
        }
#endif
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
