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

    [Header("Trigger")]
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private SpriteRenderer interactPrompt;

    [Header("Scene")]
#if UNITY_EDITOR
    [SerializeField] private SceneAsset scene;
#endif
    private string sceneName;

    public SpriteRenderer InteractPrompt { get => interactPrompt; set => interactPrompt = value; }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (scene != null)
        {
            sceneName = scene.name;
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
