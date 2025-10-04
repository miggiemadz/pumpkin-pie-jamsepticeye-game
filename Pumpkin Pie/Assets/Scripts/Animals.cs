using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour
{
    [SerializeField] private GameInformation gameInformation;
    [SerializeField] private string animalName;
    [SerializeField] private int animalType;
    [SerializeField] private Sprite[] ah;
    [SerializeField] private Sprite sh;
    private List<Sprite> headshots = new List<Sprite>();
    private List<string> dialogueText = new List<string>();

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void TriggerDialogue()
    {
        switch (animalName)
        {
            case "Chicken":
                if (animalType == 0)
                {
                    headshots.Add(ah[0]);
                    dialogueText.Add("*Cluck Cluck Cluck*");
                }
                else if (animalType == 1)
                {
                    if (!gameInformation.HasEggs)
                    {
                        Sprite[] chickenSprites1 = { ah[1], sh, ah[1] };
                        headshots.AddRange(chickenSprites1);
                        string[] chickenText1 = { "*Cluck Cluck Cluck",
                        "Hello there little one, I may have to borrow an egg or two, although I don't know if I'll give them back.",
                        "*Ba-kaw?*"};
                        dialogueText.AddRange(chickenText1);
                        gameInformation.HasEggs = true;
                    }
                    else
                    {
                        headshots.Add(ah[1]);
                        dialogueText.Add("*Ba-kaw...*");
                    }
                }
                break;
            case "Cow":
                if (animalType == 0)
                {
                    headshots.Add(ah[0]);
                    dialogueText.Add("*Moooooo*");
                }
                else if (animalType == 1)
                {
                    if (!gameInformation.HasMilk)
                    {
                        Sprite[] cowSprites1 = { ah[1], sh, ah[1] };
                        headshots.AddRange(cowSprites1);
                        string[] cowText1 = { "*Moo?*",
                        "Sorry Ms.Cow lady, but I'm going to need some of you're milk for Grandpa's Pumpkin Pie.",
                        "*MOOOO*"};
                        dialogueText.AddRange(cowText1);
                        gameInformation.HasMilk = true;
                    }
                    else
                    {
                        headshots.Add(ah[1]);
                        dialogueText.Add("*MOOOOO*");
                    }
                }
                break;
            case "Dog":
                if (!gameInformation.HasNote1)
                {
                    Sprite[] dogSprites = { ah[0], sh, ah[1] };
                    headshots.AddRange(dogSprites);
                    string[] dogTexts = {"*Panting*",
                    "Boy! Get my drawing out of your mouth!",
                    "*Woof Woof!*"};
                    dialogueText.AddRange(dogTexts);
                    gameInformation.HasNote1 = true;
                }
                else
                {
                    headshots.Add(ah[0]);
                    dialogueText.Add("Woof!");
                }
                    break;
            case "Crow":
                headshots.Add(ah[0]);
                dialogueText.Add("*KAWWW!*");
                break;
        }
    }
}
