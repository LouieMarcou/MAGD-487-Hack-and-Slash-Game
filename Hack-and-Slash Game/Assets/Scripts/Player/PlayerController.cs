using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;
    private float sprintSpeed = 0f;

    [SerializeField] private MouseLook look;
    private Vector2 mouseInput;

    private Vector2 movementInput = Vector2.zero;
    private Vector3 playerVelocity;

    private CharacterController controller;

    void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        controller.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //if (gameObject.GetComponent<PauseMenu>().getGameIsPaused())
        //{
        //    return;
        //}

        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = (transform.right * movementInput.x + transform.forward * movementInput.y);
        controller.Move(move * Time.deltaTime * (playerSpeed + sprintSpeed));

        look.ReceiveInput(mouseInput);
    }
}
