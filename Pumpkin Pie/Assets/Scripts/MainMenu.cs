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
    [SerializeField] private GameObject settingsMenu;

    [Header("Scene Info")]
#if UNITY_EDITOR
    [SerializeField] private SceneAsset newScene;
    [SerializeField] private List<SceneAsset> sceneList = new List<SceneAsset>();
#endif

    [SerializeField] private string sceneName;
    [SerializeField] private List<string> sceneNames = new List<string>();

    public string SceneName => sceneName;
    public List<string> SceneNames => sceneNames;
    void Start()
    {
        
    }

#if UNITY_EDITOR
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

    public void PlayButton()
    {
        StartCoroutine(LoadScenesCoroutine());
    }

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

    public void SettingsButton()
    {
        gameObject.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
