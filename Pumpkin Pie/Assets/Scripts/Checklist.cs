using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Checklist : MonoBehaviour
{
    [Header("Source File")]
    [SerializeField]
    private GameInformation gameInformation; // Reference to shared game state

    [Header("UI Elements")]
    [SerializeField]
    private GameObject quest1; // UI panel for quest 1
    [SerializeField]
    private GameObject quest2; // UI panel for quest 2 (ingredients)
    [SerializeField]
    private GameObject quest3; // UI panel for quest 3

    private GameInformation.Quests currentQuest; // Cached current quest

    private List<TextMeshProUGUI> ingredients = new List<TextMeshProUGUI>(); // Text labels for ingredient checklist

    private List<bool> ingredientChecks; // Booleans representing collected ingredients
 
    void Start()
    {
        // Collect the child TMP labels from the quest2 panel to represent ingredients
        ingredients.AddRange(quest2.GetComponentsInChildren<TextMeshProUGUI>());
    }

    /// <summary>
    /// Refresh UI to reflect the current quest and which ingredients have been collected.
    /// </summary>
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
                        text.fontStyle = FontStyles.Strikethrough; // Mark ingredient as collected
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
