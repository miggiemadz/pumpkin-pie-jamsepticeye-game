#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    [Header("Misc")]
    [SerializeField] private GameInformation gameInformation;
    [SerializeField] private SpriteRenderer interactPrompt;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip transitionSFX;
    [SerializeField] private AudioClip newSceneMusic;
    [SerializeField] private float fadeTime = 1.5f;

    [Header("Scene Info")]
#if UNITY_EDITOR
    [SerializeField] private SceneAsset newScene;
    [SerializeField] private List<SceneAsset> sceneList = new List<SceneAsset>();
#endif

    [SerializeField] private string sceneName;
    [SerializeField] private List<string> sceneNames = new List<string>();

    public SpriteRenderer InteractPrompt { get => interactPrompt; set => interactPrompt = value; }
    public string SceneName => sceneName;
    public List<string> SceneNames => sceneNames;

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

    public void ChangeScene()
    {
        StartCoroutine(LoadScenesCoroutine());
    }

    private IEnumerator LoadScenesCoroutine()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic(fadeTime);

            if (transitionSFX != null)
                AudioManager.Instance.PlaySFX(transitionSFX);
        }

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

        if (AudioManager.Instance != null && newSceneMusic != null)
        {
            AudioManager.Instance.PlayMusic(newSceneMusic, fadeTime);
        }
    }

}
