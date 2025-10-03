using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private InputActionReference move;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    private Vector2 moveDirection;

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
}
