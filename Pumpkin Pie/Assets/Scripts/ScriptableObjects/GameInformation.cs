using UnityEngine;

/// <summary>
/// Holds globally shared game state and configuration as a ScriptableObject.
/// This includes settings, quest state, inventory flags and other persistent data.
/// </summary>
[CreateAssetMenu(fileName = "GameInformation", menuName = "Scriptable Objects/GameInformation")]
public class GameInformation : ScriptableObject
{
    // Scene Manager
    private string lastScene;

    // Game Settings
    private float musicVolume = 10;
    private float sfxVolume = 10;

    // Initial monologue flag
    private bool initialMonologe = false;

    // Quests
    public enum Quests
    {
        Quest1, // Talk to Grandma
        Quest2, // Get Ingredients
        Quest3 // Give grandpa pie
    }

    // Grandma Checkpoints
    public enum GrandmaCheckpoints
    {
        Intro, // First ever conversation
        Ingredients, // After first conversation
        PrePie, // Before acquiring all ingredients
        PostPie // After acquiring all ingredients
    }

    // Notes collected
    private bool hasNote1 = false;
    private bool hasNote2 = false;
    private bool hasNote3 = false;
    private bool hasNote4 = false;

    // Ingredients collected
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

    // Current checkpoint and quest state
    private GrandmaCheckpoints currentCheckpoint = 0;
    private Quests currentQuests = Quests.Quest1;

    /// <summary>
    /// The last scene that was loaded before the game was saved.
    /// </summary>
    public string LastScene { get => lastScene; set => lastScene = value; }

    /// <summary>
    /// The volume of the music, from 0 to 10.
    /// </summary>
    public float MusicVolume { get => musicVolume; set => musicVolume = value; }

    /// <summary>
    /// The volume of the sound effects, from 0 to 10.
    /// </summary>
    public float SFXVolume { get => sfxVolume; set => sfxVolume = value; }

    /// <summary>
    /// Indicates if the player has collected milk.
    /// </summary>
    public bool HasMilk { get => hasMilk; set => hasMilk = value; }
    /// <summary>
    /// Indicates if the player has collected sugar.
    /// </summary>
    public bool HasSugar { get => hasSugar; set => hasSugar = value; }
    /// <summary>
    /// Indicates if the player has collected cinnamon.
    /// </summary>
    public bool HasCinammon { get => hasCinammon; set => hasCinammon = value; }
    /// <summary>
    /// Indicates if the player has collected eggs.
    /// </summary>
    public bool HasEggs { get => hasEggs; set => hasEggs = value; }
    /// <summary>
    /// Indicates if the player has collected pumpkin.
    /// </summary>
    public bool HasPumpkin { get => hasPumpkin; set => hasPumpkin = value; }

    /// <summary>
    /// Indicates if the player has the barn key.
    /// </summary>
    public bool HasBarnKey { get => hasBarnKey; set => hasBarnKey = value; }
    /// <summary>
    /// Indicates if the player has the shed key.
    /// </summary>
    public bool HasShedKey { get => hasShedKey; set => hasShedKey = value; }

    /// <summary>
    /// Indicates if the player has the toolbox.
    /// </summary>
    public bool HasToolBox { get => hasToolBox; set => hasToolBox = value; }
    /// <summary>
    /// Indicates if the player has the mice spray.
    /// </summary>
    public bool HasMiceSpray { get => hasMiceSpray; set => hasMiceSpray = value; }

    /// <summary>
    /// The current checkpoint of the player in the grandma storyline.
    /// </summary>
    public GrandmaCheckpoints CurrentCheckpoint { get => currentCheckpoint; set => currentCheckpoint = value; }
    /// <summary>
    /// Indicates if the player has baked a pumpkin pie.
    /// </summary>
    public bool HasPumpkinPie { get => hasPumpkinPie; set => hasPumpkinPie = value; }
    /// <summary>
    /// Indicates if the player has straw.
    /// </summary>
    public bool HasStraw { get => hasStraw; set => hasStraw = value; }
    /// <summary>
    /// Indicates if the player has collected note 1.
    /// </summary>
    public bool HasNote1 { get => hasNote1; set => hasNote1 = value; }
    /// <summary>
    /// Indicates if the player has collected note 2.
    /// </summary>
    public bool HasNote2 { get => hasNote2; set => hasNote2 = value; }
    /// <summary>
    /// Indicates if the player has collected note 3.
    /// </summary>
    public bool HasNote3 { get => hasNote3; set => hasNote3 = value; }
    /// <summary>
    /// Indicates if the player has collected note 4.
    /// </summary>
    public bool HasNote4 { get => hasNote4; set => hasNote4 = value; }
    /// <summary>
    /// The current quest of the player.
    /// </summary>
    public Quests CurrentQuests { get => currentQuests; set => currentQuests = value; }
    /// <summary>
    /// Indicates if the initial monologue has been played.
    /// </summary>
    public bool InitialMonologe { get => initialMonologe; set => initialMonologe = value; }
}
