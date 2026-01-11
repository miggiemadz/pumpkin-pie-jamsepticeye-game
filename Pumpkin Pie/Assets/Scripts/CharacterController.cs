using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [Header("Player Interactions")]
    [SerializeField]
    private GameManager gameManager; // Central game manager reference
    [SerializeField]
    private GameInformation gameInformation; // Shared game state
    [SerializeField]
    private Checklist checkList; // Checklist manager reference
    [SerializeField]
    private DialogueBox dialogueBox; // Dialogue UI used to show conversations
    [SerializeField]
    private InputActionReference interact; // Input action used for interactions

    private GameObject interactable; // Currently interactable object the player is near
    private string interactType; // Type identifier for the interactable (e.g., "NPC" or "Scene Changer")
    private bool isDialogueOpen; // Cached state of whether dialogue UI is currently open

    [Header("Player Movement")]
    [SerializeField]
    private float moveSpeed = 5f; // Movement speed multiplier
    [SerializeField]
    private InputActionReference move; // Input action used for movement

    [Header("Components")]
    [SerializeField]
    private Rigidbody rb; // Rigidbody used for movement
    [SerializeField]
    private Animator animator; // Animator for player animations
    private Vector2 moveDirection; // Current movement input vector

    [Header("Monologue UI")]
    [SerializeField]
    private Sprite sh; // Default headshot used for the player's monologue
    private List<string> texts = new List<string>(); // Temporary storage for monologue text
    private List<Sprite> headshots = new List<Sprite>(); // Temporary storage for monologue headshots

    private void Start()
    {
        // Cache common references from the GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dialogueBox = gameManager.DialogueBox.GetComponent<DialogueBox>();
        checkList = gameManager.Checklist.GetComponent<Checklist>();

        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        // If we are in the first playable scene and the initial monologue hasn't run yet,
        // populate and show a short monologue sequence.
        if (SceneManager.GetActiveScene().buildIndex == 1 && !gameInformation.InitialMonologe)
        {
            texts = new List<string>();
            headshots = new List<Sprite>();

            Sprite[] newHeadshots = { sh, sh, sh };
            string[] newTexts = { "*yawnnnn....",
            "All that barn work yesterday killed me.",
            "I wonder what Grandma is up to." };
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
        // Reserved for future initialization
    }

    private void OnEnable()
    {
        // Subscribe to the interact action
        interact.action.Enable();
        interact.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        // Unsubscribe and disable the interact action
        interact.action.performed -= OnInteract;
        interact.action.Disable();
    }

    /// <summary>
    /// Called when the interact input action is performed. Handles interactions with NPCs and scene changers.
    /// </summary>
    /// <param name="context">Input callback context (unused).</param>
    private void OnInteract(InputAction.CallbackContext context)
    {
        if (interactable == null) return;

        if (interactType == "NPC")
        {
            // Interact with NPC types: Grandma and Animals
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
                // Special case: door to Grandpa is locked until certain quest state
                if (interactable.name == "GrandpaDoor" && gameInformation.CurrentQuests != GameInformation.Quests.Quest3)
                {
                    if (!isDialogueOpen)
                    {
                        headshots = new List<Sprite>();
                        texts = new List<string>();

                        Sprite[] newHeadshots = { sh, sh };
                        string[] newTexts = { "Granpda is sleeping right now, I don't want to wake him up.",
                    "Let's make sure we get those ingredients for that Pumpkin Pie." };

                        headshots.AddRange(newHeadshots);
                        texts.AddRange(newTexts);

                        DialogueBox box = dialogueBox.GetComponent<DialogueBox>();
                        box.CreateDialogueBox(headshots, texts);

                        dialogueBox.gameObject.SetActive(true);
                    }
                }
                else
                {
                    // Normal scene change flow
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
            // Read movement input only when dialogue is not open
            moveDirection = move.action.ReadValue<Vector2>();
        }

        // Update animator parameters for movement blending
        animator.SetFloat("movementx", moveDirection.x);
        animator.SetFloat("movementy", moveDirection.y);
    }

    private void FixedUpdate()
    {
        if (!isDialogueOpen)
        {
            // Apply movement to the Rigidbody while preserving the vertical velocity
            rb.linearVelocity = new Vector3(
                moveDirection.x * moveSpeed,
                rb.linearVelocity.y,
                moveDirection.y * moveSpeed
            );
        }
    }

    /// <summary>
    /// Returns true when the dialogue box UI is currently active/open.
    /// </summary>
    /// <returns>True if dialogue is open; false otherwise.</returns>
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
