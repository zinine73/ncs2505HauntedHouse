using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction MoveAction;
    public float walkSpeed = 1.0f;
    public float turnSpeed = 20f;

    Rigidbody rb;
    Vector3 movement;
    Quaternion rotation = Quaternion.identity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MoveAction.Enable();
    }

    void FixedUpdate()
    {
        var pos = MoveAction.ReadValue<Vector2>();
        float horizontal = pos.x;
        float vertical = pos.y;

        movement.Set(horizontal, 0f, vertical);
        movement.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards
            (transform.forward, movement, 
            turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);

        rb.MoveRotation(rotation);
        rb.MovePosition(rb.position + movement 
            * walkSpeed * Time.deltaTime);
    }
}
