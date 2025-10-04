using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [Header("Player Interactions")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private InputActionReference interact;
    private bool isNPCInteract;
    private bool isSceneChangerInteract;

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
        interact.action.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        
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
        if (!dialogueBox.activeSelf)
        {
            if (other.CompareTag("NPC"))
            {
                isNPCInteract = true;
            }

            if (other.CompareTag("SceneChanger"))
            {
                SceneChanger sc = other.GetComponent<SceneChanger>();
                sc.InteractPrompt.gameObject.SetActive(true);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {

        }

        if (other.CompareTag("SceneChanger"))
        {
            SceneChanger sc = other.GetComponent<SceneChanger>();
            sc.InteractPrompt.gameObject.SetActive(false);
        }

    }
}
