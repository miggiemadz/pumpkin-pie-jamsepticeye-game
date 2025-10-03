using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private InputActionReference move;

    [SerializeField] private Rigidbody rb;
    private Vector2 moveDirection;

    void Start()
    {
        
    }

    void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>();

        rb.MovePosition(new Vector3(gameObject.transform.position.x + moveDirection.x * moveSpeed, 0, gameObject.transform.position.z + moveDirection.y * moveSpeed));
    }
}
