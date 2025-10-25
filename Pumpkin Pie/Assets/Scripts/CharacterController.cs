using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [Header("Player Interactions")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameInformation gameInformation;
    [SerializeField] private Checklist checkList;
    [SerializeField] private DialogueBox dialogueBox;
    [SerializeField] private InputActionReference interact;
    private GameObject interactable;
    private string interactType;
    private bool isDialogueOpen;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputActionReference move;

    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    private Vector2 moveDirection;

    [Header("Monologue UI")]
    [SerializeField] private Sprite sh;
    private List<string> texts = new List<string>();
    private List<Sprite> headshots = new List<Sprite>();

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dialogueBox = gameManager.DialogueBox.GetComponent<DialogueBox>();
        checkList = gameManager.Checklist.GetComponent<Checklist>();

        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex == 1 && !gameInformation.InitialMonologe)
        {
            texts = new List<string>();
            headshots = new List<Sprite>();

            Sprite[] newHeadshots = { sh, sh, sh };
            string[] newTexts = { "*yawnnnn....",
            "All that barn work yesterday killed me.",
            "I wonder what Grandma is up to."};
            headshots.AddRange(newHeadshots);
            texts.AddRange(newTexts);

            DialogueBox box = dialogueBox.GetComponent<DialogueBox>();
            box.CreateDialogueBox(headshots, texts);

            dialogueBox.gameObject.SetActive(true);

            gameInformation.InitialMonologe = true;
        }
    }

    private void Awake()
    {
        
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
        if (interactable == null) return;

        if (interactType == "NPC")
        {
            if (interactable.TryGetComponent(out Grandma grandma) && !isDialogueOpen)
            {
                grandma.TriggerDialogue();

                DialogueBox box = dialogueBox.GetComponent<DialogueBox>();
                box.CreateDialogueBox(grandma.Headshots, grandma.Texts);

                dialogueBox.gameObject.SetActive(true);
            }

            if (interactable.TryGetComponent(out Animals animal) && !isDialogueOpen)
            {
                animal.TriggerDialogue();

                DialogueBox box = dialogueBox.GetComponent<DialogueBox>();
                box.CreateDialogueBox(animal.Headshots, animal.DialogueText);

                dialogueBox.gameObject.SetActive(true);

                checkList.UpdateChecklist();
            }
        }

        else if (interactType == "Scene Changer")
        {
            if (interactable.TryGetComponent(out SceneChanger sc))
            {
                if (interactable.name == "GrandpaDoor" && gameInformation.CurrentQuests != GameInformation.Quests.Quest3)
                {
                    if (!isDialogueOpen)
                    {
                        headshots = new List<Sprite>();
                        texts = new List<string>();

                        Sprite[] newHeadshots = { sh, sh };
                        string[] newTexts = { "Granpda is sleeping right now, I don't want to wake him up.",
                    "Let's make sure we get those ingredients for that Pumpkin Pie."};

                        headshots.AddRange(newHeadshots);
                        texts.AddRange(newTexts);

                        DialogueBox box = dialogueBox.GetComponent<DialogueBox>();
                        box.CreateDialogueBox(headshots, texts);

                        dialogueBox.gameObject.SetActive(true);
                    }
                }
                else
                {
                    sc.ChangeScene();
                }
            }
        }
    }

    private void Update()
    {
        isDialogueOpen = IsDialogueOpen();
        if (!isDialogueOpen) 
        { 
            moveDirection = move.action.ReadValue<Vector2>();
        }

        animator.SetFloat("movementx", moveDirection.x);
        animator.SetFloat("movementy", moveDirection.y);
    }

    private void FixedUpdate()
    {
        if (!isDialogueOpen)
        {
            rb.linearVelocity = new Vector3(
                moveDirection.x * moveSpeed,
                rb.linearVelocity.y,
                moveDirection.y * moveSpeed
            );
        }
    }

    private bool IsDialogueOpen()
    {
        return dialogueBox != null && dialogueBox.gameObject.activeSelf;
    }

    private void OnTriggerEnter(Collider other)
    {
        interactable = other.gameObject;

        if (interactable.CompareTag("NPC"))
        {
            interactType = "NPC";

            if (interactable.TryGetComponent(out Grandma grandma))
            {
                grandma.InteractPrompt.gameObject.SetActive(true);
            }

            if (interactable.TryGetComponent(out Animals animal))
            {
                animal.InteractPrompt.gameObject.SetActive(true);
            }
        }

        else if (interactable.CompareTag("SceneChanger"))
        {
            interactType = "Scene Changer";

            if (interactable.TryGetComponent(out SceneChanger sc))
            {
                sc.InteractPrompt.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactable == null) return;

        if (interactable.CompareTag("NPC"))
        {
            if (interactable.TryGetComponent(out Grandma grandma))
            {
                grandma.InteractPrompt.gameObject.SetActive(false);
            }

            if (interactable.TryGetComponent(out Animals animal))
            {
                animal.InteractPrompt.gameObject.SetActive(false);
            }
        }

        if (interactable.CompareTag("SceneChanger"))
        {
            if (interactable.TryGetComponent(out SceneChanger sc))
            {
                sc.InteractPrompt.gameObject.SetActive(false);
            }
        }

        interactable = null;
        interactType = null;
    }
}
