using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu; // Settings menu to open from main menu

    [Header("Scene Info")]
#if UNITY_EDITOR
    [SerializeField]
    private SceneAsset newScene; // Editor-only SceneAsset for selecting the main scene
    [SerializeField]
    private List<SceneAsset> sceneList = new List<SceneAsset>(); // Editor-only list for additive scenes
#endif

    [SerializeField]
    private string sceneName; // Name of the primary scene to load
    [SerializeField]
    private List<string> sceneNames = new List<string>(); // Names of additional scenes to load additively

    public string SceneName => sceneName;
    public List<string> SceneNames => sceneNames;
    void Start()
    {
        // Reserved for any startup logic
    }

#if UNITY_EDITOR
    /// <summary>
    /// Editor helper to populate string scene names from SceneAsset references.
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
    /// Start loading the configured scenes and transition into the game.
    /// </summary>
    public void PlayButton()
    {
        StartCoroutine(LoadScenesCoroutine());
    }

    /// <summary>
    /// Coroutine that loads the primary scene and then any additional scenes additively.
    /// </summary>
    private IEnumerator LoadScenesCoroutine()
    {
        AsyncOperation mainLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        yield return new WaitUntil(() => mainLoad.isDone);

        foreach (var name in sceneNames)
        {
            if (name != sceneName)
            {
                AsyncOperation additiveLoad = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
                yield return new WaitUntil(() => additiveLoad.isDone);
            }
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    /// <summary>
    /// Open the settings menu and hide the main menu.
    /// </summary>
    public void SettingsButton()
    {
        gameObject.SetActive(false);
        settingsMenu.SetActive(true);
    }

    /// <summary>
    /// Quit the application. Works only in builds.
    /// </summary>
    public void QuitButton()
    {
        Application.Quit();
    }
}
