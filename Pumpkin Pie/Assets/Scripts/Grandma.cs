using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that controls Grandma NPC dialogue and interactions.
/// Stores dialogue headshots and text based on the current game checkpoint
/// and updates game state and checklist as appropriate when dialogue is triggered.
/// </summary>
public class Grandma : MonoBehaviour
{
    [Header("Interactions")]
    [SerializeField]
    private GameObject interactPrompt; // UI prompt shown when the player can interact with Grandma

    [Header("Source Files")]
    [SerializeField]
    private GameInformation gameInformation; // Reference to central game state (ScriptableObject)
    [SerializeField]
    private Checklist checklist; // Reference to the checklist manager in the scene
    private GameInformation.GrandmaCheckpoints checkpoint; // Local copy of the current grandma checkpoint

    [Header("Dialogue")]
    [SerializeField]
    private Sprite gh; // Grandma headshot sprite
    [SerializeField]
    private Sprite sh; // Other character headshot sprite (e.g., player)
    private List<Sprite> headshots = new List<Sprite>(); // Runtime list of headshots for the current dialogue
    private List<string> texts = new List<string>(); // Runtime list of dialogue lines for the current dialogue

    /// <summary>
    /// Public accessor for the current dialogue headshots. Used by dialogue UI.
    /// </summary>
    public List<Sprite> Headshots { get => headshots; set => headshots = value; }

    /// <summary>
    /// Public accessor for the current dialogue text lines. Used by dialogue UI.
    /// </summary>
    public List<string> Texts { get => texts; set => texts = value; }

    /// <summary>
    /// Public accessor for the interaction prompt GameObject.
    /// </summary>
    public GameObject InteractPrompt { get => interactPrompt; set => interactPrompt = value; }

    void Start()
    {
        // Find the Checklist component in scene by name and cache reference.
        checklist = GameObject.Find("Checklist").GetComponent<Checklist>();
    }

    void Update()
    {
        // Intentionally left empty: logic may be added later for per-frame behavior.
    }

    /// <summary>
    /// Builds the dialogue arrays (headshots and texts) based on the current checkpoint
    /// in GameInformation, updates any game state and checklist entries, and prepares
    /// data for the dialogue UI to consume.
    /// </summary>
    public void TriggerDialogue()
    {
        // Reset runtime lists
        headshots = new List<Sprite>();
        texts = new List<string>();

        checkpoint = gameInformation.CurrentCheckpoint;
        switch (checkpoint)
        {
            case GameInformation.GrandmaCheckpoints.Intro:
                // Populate dialogue for the Intro checkpoint.
                Sprite[] newHeadshots = { gh, sh, gh, sh, gh, sh, gh, gh, sh };
                headshots.AddRange(newHeadshots);
                string[] newTexts = { "Morning, sweetheat. Did you sleep well?",
                "*Yawn Loudly* Y-yeah, I was up watching jack's new video! Something about a game jam, whatever that is.",
                "You and that Youtuber. Anyways, you know what day today is correct?",
                "It's Grandpa's Birthday! Which means Pumpkin Pie!",
                "Correct dear, you know Grandpa loves Pumpkin Pie. But granny is getting too old to walk so you're going to help me ok?",
                "Of course Grandma! What are we going to need?",
                "Nothing crazy; we just need some pumpkin from the field, sugar & cinnamon from the shed, and some milk & eggs from the barn.",
                "Here is a key to the barn and hey, maybe you could pick up all those drawings you and Grandpa kept leaving around.",
                "Okay Grandma. I'll find everything so fast, you won't even realize I'm gone."};
                texts.AddRange(newTexts);

                // Advance game state and update checklist
                gameInformation.CurrentCheckpoint = GameInformation.GrandmaCheckpoints.Ingredients;
                gameInformation.CurrentQuests = GameInformation.Quests.Quest2;
                checklist.UpdateChecklist();
                gameInformation.HasBarnKey = true;
                break;

            case GameInformation.GrandmaCheckpoints.Ingredients:
                // Reminder dialogue when player is collecting ingredients
                headshots.Add(gh);
                texts.Add("Remember, all we need are milk & eggs from the barn, sugar and cinnamon from the shed and pumkpin from the field.");
                break;

            case GameInformation.GrandmaCheckpoints.PrePie:
                // Dialogue leading up to pie making
                Sprite[] newHeadshots1 = { sh, gh, sh, gh };
                headshots.AddRange(newHeadshots1);
                string[] newTexts1 = { "Grandma! Grandma! I got everything, took me a little while but I found every ingredient.",
                "Lovely dear, now place them on the counter so Grandma can get to work.",
                "Can I help you make it?",
                "Of course, lets hurry, it's getting late."};
                texts.AddRange(newTexts1);

                // Move to post-pie checkpoint
                gameInformation.CurrentCheckpoint = GameInformation.GrandmaCheckpoints.PostPie;
                break;

            case GameInformation.GrandmaCheckpoints.PostPie:
                // Post-pie dialogue and finalization
                Sprite[] newHeadshots2 = { sh, gh, sh, gh, sh };
                headshots.AddRange(newHeadshots2);
                string[] newTexts2 = { "Mmmmmm, that smells soo good grandma!",
                "You're Grandpa's favorite Pumpkin Pie, I just know he will love it.",
                "Can I go give it to him now?",
                "Yes but dear, please be quiet you don't want to disturb him.",
                "Ok Grandma!"};
                texts.AddRange(newTexts2);
                gameInformation.HasPumpkinPie = true;
                break;
        }
    }
}
