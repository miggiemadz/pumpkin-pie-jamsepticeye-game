using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : MonoBehaviour
{
    [Header("Interactions")]
    [SerializeField] private GameObject interactPrompt;
    private bool isInteracted;

    [Header("Checkpoints")]
    [SerializeField] private GameInformation gameInformation;
    private string checkpoint;

    [Header("Dialogue")]
    [SerializeField] private Sprite gh;
    [SerializeField] private Sprite sh;
    private List<Sprite> headshots = new List<Sprite>();
    private List<string> texts = new List<string>();

    public List<Sprite> Headshots { get => headshots; set => headshots = value; }
    public List<string> Texts { get => texts; set => texts = value; }
    public bool IsInteracted { get => isInteracted; set => isInteracted = value; }
    public GameObject InteractPrompt { get => interactPrompt; set => interactPrompt = value; }

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void TriggerDialogue()
    {
        checkpoint = gameInformation.CurrentCheckpoint.ToString();
        if (IsInteracted)
        {
            switch (checkpoint)
            {
                case "Intro":
                    Sprite[] newHeadshots = { gh, sh, gh, sh, gh, sh, gh, gh, sh };
                    headshots.AddRange(newHeadshots);
                    string[] newTexts = { "Morning, sweetheat. Did you sleep well?",
                    "*Yawn Loudly* Y-yeah, I was up watching jack's new video! Something about a game jam, whatever that is.",
                    "You and that Youtuber. Anyways, you know what day today is correct?",
                    "It's Grandpa's Birthday! Which means Pumpkin Pie!",
                    "Of course, you know Grandpa loves Pumpkin Pie. But granny is getting too old to walk so you're going to help me ok?",
                    "Of course Grandma! What are we going to need?",
                    "Nothing crazy; we just need some pumpkin from the field, sugar & cinnamon from the shed, and some milk & eggs from the barn.",
                    "And hey, maybe you could pick up all those drawings you and Grandpa kept leaving around.",
                    "Okay Grandma. I'll find everything so fast, you won't even realize I'm gone."};
                    texts.AddRange(newTexts);
                    break;
                case "Ingredients":
                    break;
                case "Pre-Pumpkin Pie":
                    break;
                case "Post-Pumpkin Pie":
                    break;
            }
            IsInteracted = false;
        }
    }
}
