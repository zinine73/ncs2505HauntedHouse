using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction MoveAction;
    public float walkSpeed = 1.0f;
    public float turnSpeed = 20f;

    Rigidbody rb;
    Animator anim;
    Vector3 movement;
    Quaternion rotation = Quaternion.identity;
    AudioSource audioSource;
    List<string> ownedKeys = new List<string>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        MoveAction.Enable();
    }

    void FixedUpdate()
    {
        var pos = MoveAction.ReadValue<Vector2>();
        float horizontal = pos.x;
        float vertical = pos.y;

        movement.Set(horizontal, 0f, vertical);
        movement.Normalize();
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        anim.SetBool("WALK", isWalking);
        
        Vector3 desiredForward = Vector3.RotateTowards
            (transform.forward, movement, 
            turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);

        rb.MoveRotation(rotation);
        rb.MovePosition(rb.position + movement 
            * walkSpeed * Time.deltaTime);

        if (isWalking)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void AddKey(string keyName)
    {
        ownedKeys.Add(keyName);
    }

    public bool OwnKey(string keyName)
    {
        return ownedKeys.Contains(keyName);
    }
}
