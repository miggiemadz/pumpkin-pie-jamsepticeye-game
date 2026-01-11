using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// UI component that displays a sequence of dialogue lines with associated headshots.
/// The dialogue advances when the player performs the configured interact input action.
/// </summary>
public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    private InputActionReference interact; // Input action used to advance dialogue

    [SerializeField]
    private Image headshot; // UI image used to show the speaker's portrait
    [SerializeField]
    private TextMeshProUGUI text; // TMP label used to render dialogue text

    // Runtime lists that back the dialogue sequence
    private List<Sprite> headshots = new List<Sprite>();
    private List<string> texts = new List<string>(); 

    private int pointer; // Current index into the dialogue lists
    private int maxPointer; // Number of dialogue entries

    void Start()
    {
        pointer = 0; // Ensure pointer is initialized
    }

    private void OnEnable()
    {
        // Enable and subscribe to the interact input action so the player can advance dialogue.
        interact.action.Enable();
        interact.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        interact.action.performed -= OnInteract;
    }

    /// <summary>
    /// Input callback invoked when the interact action is performed; advances the dialogue pointer.
    /// </summary>
    /// <param name="context">Input callback context (ignored).</param>
    private void OnInteract(InputAction.CallbackContext context)
    {
        pointer++;
    }

    void Update()
    {
        // If we have more lines remaining, update the UI to show the current line.
        if (pointer < maxPointer && headshots.Count > 0 && texts.Count > 0)
        {
            headshot.sprite = headshots[pointer];
            text.text = texts[pointer];
        }

        // If the player advanced past the last line, clear dialogue and hide the UI.
        if (pointer == maxPointer)
        {
            headshots.Clear();
            texts.Clear();
            pointer = 0;
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Initializes the dialogue box content with the provided headshots and text lines.
    /// The dialogue box will display the provided content when enabled.
    /// </summary>
    /// <param name="headshots">List of headshot sprites for each line.</param>
    /// <param name="texts">List of text strings for each line.</param>
    public void CreateDialogueBox(List<Sprite> headshots, List<string> texts)
    {
        this.headshots = new List<Sprite>(headshots);
        this.texts = new List<string>(texts);
        maxPointer = headshots.Count;
    }
}
