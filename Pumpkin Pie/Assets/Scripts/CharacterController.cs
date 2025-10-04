using Unity.VisualScripting;
using System.Collections.Generic;
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
    private bool isDialogueOpen;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 5f;
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
        interact.action.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (interactable == null) return;

        if (interactType == "NPC")
        {
            if (interactable.TryGetComponent(out Grandma grandma) && !isDialogueOpen)
            {
                grandma.IsInteracted = true;
                grandma.TriggerDialogue();

                DialogueBox box = dialogueBox.GetComponent<DialogueBox>();
                box.CreateDialogueBox(grandma.Headshots, grandma.Texts);

                dialogueBox.SetActive(true);
            }
        }

        else if (interactType == "Scene Changer")
        {
            if (interactable.TryGetComponent(out SceneChanger sc))
            {
                sc.ChangeScene();
            }
        }
    }

    private void Update()
    {
        isDialogueOpen = IsDialogueOpen();

        moveDirection = move.action.ReadValue<Vector2>();

        animator.SetFloat("movementx", moveDirection.x);
        animator.SetFloat("movementy", moveDirection.y);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(
            moveDirection.x * moveSpeed,
            rb.linearVelocity.y,
            moveDirection.y * moveSpeed
        );
    }

    private bool IsDialogueOpen()
    {
        return dialogueBox != null && dialogueBox.activeSelf;
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
