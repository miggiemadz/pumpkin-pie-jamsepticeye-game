using UnityEngine;

[CreateAssetMenu(fileName = "GameInformation", menuName = "Scriptable Objects/GameInformation")]
public class GameInformation : ScriptableObject
{
    // Scene Manager
    private string lastScene;

    // Game Settings
    private float musicVolume = 10;
    private float sfxVolume = 10;

    // Grandma Checkpoints
    public enum GrandmaCheckpoints
    {
        Intro,
        Ingredients,
        PrePie,
        PostPie
    }

    // Notes
    private bool hasNote1 = false;
    private bool hasNote2 = false;
    private bool hasNote3 = false;
    private bool hasNote4 = false;

    // Ingredients
    private bool hasMilk = false;
    private bool hasSugar = false;
    private bool hasCinammon = false;
    private bool hasEggs = false;
    private bool hasPumpkin = false;

    // Keys
    private bool hasBarnKey = false;
    private bool hasShedKey = false;

    // Tools
    private bool hasToolBox = false;
    private bool hasMiceSpray = false;
    private bool hasStraw = false;

    // Ending
    private bool hasPumpkinPie = false;

    private GrandmaCheckpoints currentCheckpoint = 0;

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

    public GrandmaCheckpoints CurrentCheckpoint { get => currentCheckpoint; set => currentCheckpoint = value; }
    public bool HasPumpkinPie { get => hasPumpkinPie; set => hasPumpkinPie = value; }
    public bool HasStraw { get => hasStraw; set => hasStraw = value; }
    public bool HasNote1 { get => hasNote1; set => hasNote1 = value; }
    public bool HasNote2 { get => hasNote2; set => hasNote2 = value; }
    public bool HasNote3 { get => hasNote3; set => hasNote3 = value; }
    public bool HasNote4 { get => hasNote4; set => hasNote4 = value; }
}
