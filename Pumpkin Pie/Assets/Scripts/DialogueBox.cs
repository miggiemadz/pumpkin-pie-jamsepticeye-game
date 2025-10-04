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

    [SerializeField] private List<Sprite> headshots;
    [SerializeField] private List<string> texts;
    private int pointer;
    private int maxPointer;

    void Start()
    {
        pointer = 0;
        maxPointer = texts.Count;
    }

    private void OnEnable()
    {
        interact.action.Enable();
        interact.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        interact.action.performed -= OnInteract;
        interact.action.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        pointer++;
    }

    void Update()
    {
        if (pointer < maxPointer)
        {
            headshot.sprite = headshots[pointer];
            text.text = texts[pointer];
        }
    }
}
