using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private InputActionReference interact;

    [SerializeField] private Image headshot;
    [SerializeField] private TextMeshProUGUI text;

    private List<Sprite> headshots = new List<Sprite>();
    private List<string> texts = new List<string>(); 
    private int pointer;
    private int maxPointer;

    void Start()
    {
        pointer = 0;
    }

    private void OnEnable()
    {
        interact.action.Enable();
        interact.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        interact.action.performed -= OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        pointer++;
    }

    void Update()
    {
        if (pointer < maxPointer && headshots.Count > 0 && texts.Count > 0)
        {
            headshot.sprite = headshots[pointer];
            text.text = texts[pointer];
        }

        if (pointer == maxPointer)
        {
            headshots.Clear();
            texts.Clear();
            pointer = 0;
            gameObject.SetActive(false);
        }
    }

    public void CreateDialogueBox(List<Sprite> headshots, List<string> texts)
    {
        this.headshots = new List<Sprite>(headshots);
        this.texts = new List<string>(texts);
        maxPointer = headshots.Count;
    }
}
