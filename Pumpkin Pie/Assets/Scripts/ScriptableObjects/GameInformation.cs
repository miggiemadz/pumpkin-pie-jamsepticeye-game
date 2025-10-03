using UnityEngine;

[CreateAssetMenu(fileName = "GameInformation", menuName = "Scriptable Objects/GameInformation")]
public class GameInformation : ScriptableObject
{
    // Scene Manager
    private string lastScene;

    // Game Settings
    private float musicVolume = 10;
    private float sfxVolume = 10;

    // Ingredients
    private bool hasMilk;
    private bool hasSugar;
    private bool hasCinammon;
    private bool hasEggs;
    private bool hasPumpkin;

    // Keys
    private bool hasBarnKey;
    private bool hasShedKey;

    // Tools
    private bool hasToolBox;
    private bool hasMiceSpray;

    public string LastScene { get => lastScene; set => lastScene = value; }

    public float MusicVolume { get => musicVolume; set => musicVolume = value; }
    public float SFXVolume { get => sfxVolume; set => sfxVolume = value; }

    public bool HasMilk { get => hasMilk; set => hasMilk = value; }
    public bool HasSugar { get => hasSugar; set => hasSugar = value; }
    public bool HasCinammon { get => hasCinammon; set => hasCinammon = value; }
    public bool HasEggs { get => hasEggs; set => hasEggs = value; }
    public bool HasPumpkin { get => hasPumpkin; set => hasPumpkin = value; }

    public bool HasBarnKey { get => hasBarnKey; set => hasBarnKey = value; }
    public bool HasShedKey { get => hasShedKey; set => hasShedKey = value; }

    public bool HasToolBox { get => hasToolBox; set => hasToolBox = value; }
    public bool HasMiceSpray { get => hasMiceSpray; set => hasMiceSpray = value; }
}
