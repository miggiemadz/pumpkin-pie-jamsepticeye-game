using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Checklist : MonoBehaviour
{
    [Header("Source File")]
    [SerializeField] private GameInformation gameInformation;

    [Header("UI Elements")]
    [SerializeField] private GameObject quest1;
    [SerializeField] private GameObject quest2;
    [SerializeField] private GameObject quest3;

    private GameInformation.Quests currentQuest;

    private List<TextMeshProUGUI> ingredients = new List<TextMeshProUGUI>();

    private List<bool> ingredientChecks;
 
    void Start()
    {
        ingredients.AddRange(quest2.GetComponentsInChildren<TextMeshProUGUI>());
    }

    public void UpdateChecklist()
    {
        currentQuest = gameInformation.CurrentQuests;
        ingredientChecks = new List<bool>();
        bool[] checklist = { gameInformation.HasMilk, gameInformation.HasEggs, gameInformation.HasSugar, gameInformation.HasCinammon, gameInformation.HasPumpkin };
        ingredientChecks.AddRange(checklist);

        switch (currentQuest)
        {
            case GameInformation.Quests.Quest1:
                quest1.SetActive(true);
                quest2.SetActive(false);
                quest3.SetActive(false);
                break;

            case GameInformation.Quests.Quest2:
                quest1.SetActive(false);
                quest2.SetActive(true);
                quest3.SetActive(false);

                int counter = 0;
                foreach(TextMeshProUGUI text in ingredients)
                {
                    if (ingredientChecks[counter])
                    {
                        text.fontStyle = FontStyles.Strikethrough;
                    }
                    counter++;
                }
                break;

            case GameInformation.Quests.Quest3:
                quest1.SetActive(false);
                quest2.SetActive(false);
                quest3.SetActive(true);
                break;
        }
    }
}
