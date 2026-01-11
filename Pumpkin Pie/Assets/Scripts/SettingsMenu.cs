using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages in-game settings UI such as music and SFX volumes. Persists as a singleton
/// so settings UI can be reused across scenes.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu Instance { get; private set; }

    [Header("Other")]
    [SerializeField]
    private GameInformation gameInformation; // Reference to persistent settings storage
    [SerializeField]
    private GameObject previousMenu; // Menu to return to when closing settings
    [SerializeField]
    private GameManager gameManager; // Reference used to obtain the previous menu

    [Header("Music")]
    [SerializeField]
    private Slider musicSlider; // Slider controlling music volume
    [SerializeField]
    private RawImage musicOnImage; // Icon shown when music is enabled
    [SerializeField]
    private RawImage musicOffImage; // Icon shown when music is muted
    [SerializeField]
    private TextMeshProUGUI musicText; // Text label showing numerical music volume

    [Header("SFX")]
    [SerializeField]
    private Slider SFXSlider; // Slider controlling SFX volume
    [SerializeField]
    private RawImage SFXOnImage; // Icon shown when SFX is enabled
    [SerializeField]
    private RawImage SFXOffImage; // Icon shown when SFX is muted
    [SerializeField]
    private TextMeshProUGUI SFXText; // Text label showing numerical SFX volume

    private void Awake()
    {
        // Singleton pattern to persist settings menu across scenes
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
        // Initialize UI from the stored game information values
        musicSlider.value = gameInformation.MusicVolume;
        SFXSlider.value = gameInformation.SFXVolume;

        previousMenu = gameManager.PreviousMenu;
    }

    void Update()
    {
        // Update visuals and stored settings every frame based on slider values
        musicOffImage.gameObject.SetActive(musicSlider.value <= 0f);
        musicOnImage.gameObject.SetActive(musicSlider.value > 0f);

        SFXOffImage.gameObject.SetActive(SFXSlider.value <= 0f);
        SFXOnImage.gameObject.SetActive(SFXSlider.value > 0f);

        musicText.text = musicSlider.value.ToString();
        SFXText.text = SFXSlider.value.ToString();

        gameInformation.MusicVolume = musicSlider.value;
        gameInformation.SFXVolume = SFXSlider.value;
    }

    /// <summary>
    /// Called when the Back button is pressed. Restores the previous menu and hides settings.
    /// </summary>
    public void BackButton()
    {
        previousMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
