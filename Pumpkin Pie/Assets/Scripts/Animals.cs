using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour
{
    [Header("Interactions")]
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private GameInformation gameInformation;

    [Header("Animal Info")]
    [SerializeField] private string animalName;
    [SerializeField] private int animalType;
    [SerializeField] private Sprite ah;
    [SerializeField] private Sprite sh;
    [SerializeField] private Animator animator;

    
    private List<Sprite> headshots = new List<Sprite>();
    private List<string> dialogueText = new List<string>();

    public GameObject InteractPrompt { get => interactPrompt; set => interactPrompt = value; }
    public List<Sprite> Headshots { get => headshots; set => headshots = value; }
    public List<string> DialogueText { get => dialogueText; set => dialogueText = value; }
    public string AnimalName { get => animalName; set => animalName = value; }
    public int AnimalType { get => animalType; set => animalType = value; }

    void Start()
    {
        animator.SetInteger("type", AnimalType);
    }
    void Update()
    {
        
    }

    public void TriggerDialogue()
    {
        headshots = new List<Sprite>();
        dialogueText = new List<string>();

        switch (AnimalName)
        {
            case "Chicken":
                if (AnimalType == 0)
                {
                    Headshots.Add(ah);
                    DialogueText.Add("*Cluck Cluck Cluck*");
                }
                else if (AnimalType == 1)
                {
                    if (!gameInformation.HasEggs)
                    {
                        Sprite[] chickenSprites1 = { ah, sh, ah };
                        Headshots.AddRange(chickenSprites1);
                        string[] chickenText1 = { "*Cluck Cluck Cluck",
                        "Hello there little one, I may have to borrow an egg or two, although I don't know if I'll give them back.",
                        "*Ba-kaw?*"};
                        DialogueText.AddRange(chickenText1);
                        gameInformation.HasEggs = true;
                    }
                    else
                    {
                        Headshots.Add(ah);
                        DialogueText.Add("*Ba-kaw...*");
                    }
                }
                break;
            case "Cow":
                if (AnimalType == 0)
                {
                    Headshots.Add(ah);
                    DialogueText.Add("*Moooooo*");
                }
                else if (AnimalType == 1)
                {
                    if (!gameInformation.HasMilk)
                    {
                        Sprite[] cowSprites1 = { ah, sh, ah };
                        Headshots.AddRange(cowSprites1);
                        string[] cowText1 = { "*Moo?*",
                        "Sorry Ms.Cow lady, but I'm going to need some of you're milk for Grandpa's Pumpkin Pie.",
                        "*MOOOO*"};
                        DialogueText.AddRange(cowText1);
                        gameInformation.HasMilk = true;
                    }
                    else
                    {
                        Headshots.Add(ah);
                        DialogueText.Add("*MOOOOO*");
                    }
                }
                break;
            case "Dog":
                if (!gameInformation.HasNote1)
                {
                    Sprite[] dogSprites = { ah, sh, ah };
                    Headshots.AddRange(dogSprites);
                    string[] dogTexts = {"*Panting*",
                    "Boy! Get my drawing out of your mouth!",
                    "*Woof Woof!*"};
                    DialogueText.AddRange(dogTexts);
                    gameInformation.HasNote1 = true;
                }
                else
                {
                    Headshots.Add(ah);
                    DialogueText.Add("Woof!");
                }
                    break;
            case "Crow":
                Headshots.Add(ah);
                DialogueText.Add("*KAWWW!*");
                break;
        }
    }
}
