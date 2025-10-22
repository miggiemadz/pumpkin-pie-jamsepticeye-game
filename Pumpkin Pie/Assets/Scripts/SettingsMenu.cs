using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu Instance { get; private set; }

    [Header("Other")]
    [SerializeField] private GameInformation gameInformation;
    [SerializeField] private GameObject previousMenu;
    [SerializeField] private GameManager gameManager;

    [Header("Music")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private RawImage musicOnImage;
    [SerializeField] private RawImage musicOffImage;
    [SerializeField] private TextMeshProUGUI musicText;

    [Header("SFX")]
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private RawImage SFXOnImage;
    [SerializeField] private RawImage SFXOffImage;
    [SerializeField] private TextMeshProUGUI SFXText;

    private void Awake()
    {
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
        musicSlider.value = gameInformation.MusicVolume;
        SFXSlider.value = gameInformation.SFXVolume;

        previousMenu = gameManager.PreviousMenu;
    }

    void Update()
    {
        musicOffImage.gameObject.SetActive(musicSlider.value <= 0f);
        musicOnImage.gameObject.SetActive(musicSlider.value > 0f);

        SFXOffImage.gameObject.SetActive(SFXSlider.value <= 0f);
        SFXOnImage.gameObject.SetActive(SFXSlider.value > 0f);

        musicText.text = musicSlider.value.ToString();
        SFXText.text = SFXSlider.value.ToString();

        gameInformation.MusicVolume = musicSlider.value;
        gameInformation.SFXVolume = SFXSlider.value;
    }

    public void BackButton()
    {
        previousMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
