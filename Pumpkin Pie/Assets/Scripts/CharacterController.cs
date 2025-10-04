using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [Header("Player Interactions")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private InputActionReference interact;
    private GameObject interactable;
    private string interactType;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private InputActionReference move;

    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    private Vector2 moveDirection;

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
        if (interactable != null && !dialogueBox.activeSelf)
        {
            if (interactType == "NPC")
            {
                if (interactable.GetComponent<Grandma>() != null)
                {
                    Grandma grandma = interactable.GetComponent <Grandma>();
                    grandma.IsInteracted = true;
                    grandma.TriggerDialogue();
                    dialogueBox.GetComponent<DialogueBox>().CreateDialogueBox(grandma.Headshots, grandma.Texts);
                }
                dialogueBox.SetActive(true);
            }

            if (interactType == "Scene Changer")
            {
                SceneManager.LoadScene(interactable.GetComponent<SceneChanger>().SceneName);
            }
        }
    }

    void Start()
    {
        
    }

    private void Update()
    {
        
        moveDirection = move.action.ReadValue<Vector2>();

        animator.SetFloat("movementx", moveDirection.x);
        animator.SetFloat("movementy", moveDirection.y);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.y * moveSpeed); // Keep gravity on Y
    }

    private void OnTriggerEnter(Collider other)
    {
        interactable = other.gameObject;

        if (interactable.CompareTag("NPC"))
        {
            interactType = "NPC";

            if (other.GetComponent<Grandma>() != null)
            {
                Grandma grandma = other.GetComponent<Grandma>();
                grandma.InteractPrompt.gameObject.SetActive(true);
            }
        }

        if (interactable.CompareTag("SceneChanger"))
        {
            interactType = "Scene Changer";

            SceneChanger sc = interactable.GetComponent<SceneChanger>();
            sc.InteractPrompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactable.CompareTag("NPC"))
        {
            if (other.GetComponent<Grandma>() != null)
            {
                Grandma grandma = other.GetComponent<Grandma>();
                grandma.InteractPrompt.gameObject.SetActive(false);
            }
        }

        if (interactable.CompareTag("SceneChanger"))
        {
            SceneChanger sc = interactable.GetComponent<SceneChanger>();
            sc.InteractPrompt.gameObject.SetActive(false);
        }

        interactable = null;
        interactType = null;
    }
}
