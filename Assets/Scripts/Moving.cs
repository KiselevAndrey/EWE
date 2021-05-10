using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [Header("Movement config")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotateSpeed = 100f;

    [Header("Reference")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;

    [Header("Gravity")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float graviScale;

    private float gravity;
    private bool jumping;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        animator.SetFloat("HorizontalMove", inputH);
        animator.SetFloat("VerticalMove", inputV);
        animator.SetBool("Moving", inputH != 0 || inputV != 0);

        Vector3 moveDirection = transform.forward * inputV + transform.right * inputH;
        if (moveDirection.magnitude > 1) moveDirection.Normalize();

        if (characterController.isGrounded)
        {
            jumping = Input.GetButton("Jump");
            gravity = jumping ? jumpHeight : 0;
        }
        else
        {
            if (characterController.velocity.y == 0 && jumping) gravity = -0.01f;
            else gravity += graviScale * Physics.gravity.y * Time.deltaTime;
        }
        moveDirection.y = gravity;

        characterController.Move(moveDirection * Time.deltaTime * moveSpeed);
        
    }

    private void Rotate()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseHorizontal * rotateSpeed * Time.deltaTime);
        Cursor.lockState = CursorLockMode.Locked;
    }


}
