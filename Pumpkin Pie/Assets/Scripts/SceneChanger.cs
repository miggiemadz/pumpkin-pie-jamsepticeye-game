#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using AudioManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Misc")]
    [SerializeField] private GameInformation gameInformation; // Reference to shared game state, if needed by transition logic
    [SerializeField] private SpriteRenderer interactPrompt; // Optional visual prompt when player can interact

    [Header("Audio Settings")]
    [SerializeField] private AudioClip transitionSFX; // One-shot SFX played during the transition
    [SerializeField] private AudioClip newSceneMusic; // Music to play after the scene loads
    [SerializeField] private float fadeTime = 1.5f; // Fade duration for crossfading music

    [Header("Scene Info")]
#if UNITY_EDITOR
    // SceneAsset references used only in the editor to aid selection
    [SerializeField] private SceneAsset newScene;
    [SerializeField] private List<SceneAsset> sceneList = new List<SceneAsset>();
#endif
    [SerializeField] private string lastScene; // Name of the last active scene before transition
    [SerializeField] private string sceneName; // Target scene name to load
    [SerializeField] private List<string> sceneNames = new List<string>(); // Additional scenes to load additively

    /// <summary>
    /// Public accessor for the interaction prompt renderer.
    /// </summary>
    public SpriteRenderer InteractPrompt { get => interactPrompt; set => interactPrompt = value; }

    /// <summary>
    /// Target scene name to load.
    /// </summary>
    public string SceneName => sceneName;

    /// <summary>
    /// Additional scene names configured to be loaded additively.
    /// </summary>
    public List<string> SceneNames => sceneNames;

#if UNITY_EDITOR
    /// <summary>
    /// Editor-time validation to populate string names from serialized SceneAsset references.
    /// This helps the designer keep a list of scene names in sync with selected scene assets.
    /// </summary>
    private void OnValidate()
    {
        if (newScene != null)
        {
            sceneName = newScene.name;
        }

        sceneNames.Clear();
        foreach (var sceneAsset in sceneList)
        {
            if (sceneAsset != null)
                sceneNames.Add(sceneAsset.name);
        }
    }
#endif

    /// <summary>
    /// Public method to begin the scene change process.
    /// </summary>
    public void ChangeScene()
    {
        StartCoroutine(LoadScenesCoroutine());
    }

    /// <summary>
    /// Coroutine that performs the scene loading sequence. Stops current music, optionally plays
    /// a transition SFX, loads the main scene, loads additional additive scenes, and then starts
    /// the new scene music.
    /// </summary>
    private IEnumerator LoadScenesCoroutine()
    {
        lastScene = SceneManager.GetActiveScene().name;

        if (AudioManager.Instance != null)
        {
            // Fade out current music
            AudioManager.Instance.StopMusic(fadeTime);

            // Optionally play a transition SFX
            if (transitionSFX != null)
                AudioManager.Instance.PlaySFX(transitionSFX);
        }

        // Load the main scene (single mode)
        AsyncOperation mainLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        yield return new WaitUntil(() => mainLoad.isDone);

        // Load additional configured scenes additively
        foreach (var name in sceneNames)
        {
            if (name != sceneName)
            {
                AsyncOperation additiveLoad = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
                yield return new WaitUntil(() => additiveLoad.isDone);
            }
        }

        // Ensure the main scene is set to active
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        // Start the new scene music if provided
        if (AudioManager.Instance != null && newSceneMusic != null)
        {
            AudioManager.Instance.PlayMusic(newSceneMusic, fadeTime);
        }
    }

}
