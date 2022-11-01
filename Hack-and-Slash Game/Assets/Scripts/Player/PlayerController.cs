using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;
    private float originalSpeed;
    private float sprintSpeed = 5f;
    private bool isSprinting;

    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    [SerializeField] private MouseLook look;
    private Vector2 mouseInput;

    private Vector2 movementInput = Vector2.zero;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool jumped;

    private CharacterController controller;

    [SerializeField] private float health = 100f;
    private float healthMax;


    private Coroutine staminaRegen;
    private WaitForSeconds staminaRegentick = new WaitForSeconds(0.05f);
    private WaitForSeconds staminaRegenWait = new WaitForSeconds(2f);
    private float staminaMax = 100f;
    private float currentStamina;
    private float staminaDrain = 15f;
    public Slider staminaBar;

    [SerializeField] private GameObject Weapon;
    private bool isAttacking;

    private bool isPaused;

    [SerializeField] Animator animator;

    [SerializeField] private Transform weaponObjectsContainer;

    public List<UpgradeData> upgrades;

    void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        controller.enabled = true;
        currentStamina = staminaMax;

        healthMax = health;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Movement function
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    //Looking around with the mouse function
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }

    //Attack function
    public void OnAttack(InputAction.CallbackContext context)
    {
        isAttacking = context.ReadValueAsButton();
        isAttacking = context.action.triggered;
        if(isAttacking)
        {
            Weapon.GetComponent<WeaponBase>().Attack();
            
            
        }
    }

    //Jump function
    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.ReadValueAsButton();
        jumped = context.action.triggered;
        Debug.Log("Jumped");
    }

    //Sprint function: Sets isSprinting to true
    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
        isSprinting = context.action.triggered;

        if(context.performed)
        {
            Debug.Log("is sprinting");
            isSprinting = true;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        isPaused = context.ReadValueAsButton();
        isPaused = context.action.triggered;
        if(isPaused)
        {
            mouseInput = Vector2.zero;
            gameObject.GetComponent<MouseLook>().enabled = false;
            gameObject.GetComponent<PauseMenu>().checkIfPaused();
        }
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (isSprinting)
            UpdateStamina();

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        } 
        Vector3 move = (transform.right * movementInput.x + transform.forward * movementInput.y);
        controller.Move(move * Time.deltaTime * (playerSpeed + sprintSpeed));

        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        look.ReceiveInput(mouseInput);
    }

    //Makes player sprint or cancels it
    public void UpdateSprint()
    {
        if (isSprinting)
        {
            sprintSpeed = 5;
        }
        else if (isSprinting == false)
        {
            sprintSpeed = 0;
        }
    }

    //Sets isSprinting to false 
    private void CancelSprint()
    {
        isSprinting = false;
        UpdateSprint();
    }

    //If the player is sprinting and is not out of stamina
    //Their stamina will drain
    private void UpdateStamina()
    {
        if (isSprinting)
        {
            if (currentStamina >= 0)
            {
                currentStamina -= staminaDrain * Time.deltaTime;
                staminaBar.value = currentStamina;
                if (staminaRegen != null)
                {
                    StopCoroutine(staminaRegen);
                }
                staminaRegen = StartCoroutine(RegenStamina());
            }
            else
            {
                Debug.Log("Not enough stamina");
                CancelSprint();
            }
        }
    }

    //While stamina is not at full the player will recover stamina
    private IEnumerator RegenStamina()
    {
        yield return staminaRegenWait;

        while (currentStamina < staminaMax)
        {
            currentStamina += staminaMax / 100;
            staminaBar.value = currentStamina;
            yield return staminaRegentick;
        }
        staminaRegen = null;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }


    //Sets Weapon
    public void SetWeapon(GameObject weapon)
    {
        Weapon = weapon;
    }

    //Gets animator
    public Animator GetAnimator()
    {
        return animator;
    }
}
