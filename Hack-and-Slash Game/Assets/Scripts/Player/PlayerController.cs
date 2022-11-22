using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Data and Stats")]
    public PlayerData playerData;
    public PlayerStats playerStats;
    private PlayerStats orginialPlayerStats;
	
    [Header("Speed")]
    [SerializeField] private float playerSpeed = 10.0f;
    private float originalSpeed;
    [SerializeField] private float sprintSpeed = 5f;
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

    [Header("Health")]
    [SerializeField] private float health = 100f;
    private float healthMax;
	private Coroutine healthRegen;
    private float healthRegenAmount = 2f;
	private WaitForSeconds healthRegenTick = new WaitForSeconds(5f);//varaible needs testing
	public Slider healthBar;

    //[Header("Stamina")]
    private Coroutine staminaRegen;
    private WaitForSeconds staminaRegenTick = new WaitForSeconds(0.1f);//needs testing
    private WaitForSeconds staminaRegenWait = new WaitForSeconds(2f);
    public float staminaMax = 100f;
    public float currentStamina;
    private float staminaDrain = 15f;
    public Slider staminaBar;

    [Header("Weapon")]
    [SerializeField] private GameObject Weapon;
    private bool isAttacking;

    private bool isPaused;

    [SerializeField] Animator animator;

    [SerializeField] private Transform weaponObjectsContainer;

    public List<UpgradeData> upgrades;

    private bool hasCrit = false;
    private float critMultiplier = 0f;
    private float critChance = 0f;

    private bool hasThorns = false;
    private float reflectPercentage = 0f;
	
    void Awake()
    {
		SetData(playerData);
		StoreOrginialData(playerData);
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
        //if(context.action.triggered)
        //    mouseInput = context.ReadValue<Vector2>();
        
        mouseInput = context.ReadValue<Vector2>();
    }

    //Attack function
    public void OnAttack(InputAction.CallbackContext context)
    {
        if(gameObject.GetComponent<PauseMenu>().getGameIsPaused() || Weapon == null)
        {
            return;
        }
		var actuationTime = context.time - context.startTime;

        isAttacking = context.ReadValueAsButton();

        if (context.performed)
        {
            //Debug.Log("started");
            // Debug.Log(actuationTime);
            //Debug.Log(context.interaction);
            if (Weapon.GetComponent<Axe>() && Weapon.GetComponent<Axe>().GetCanCharge() && actuationTime > 0.2)
            {
                //Debug.Log(actuationTime);

                //Debug.Log(context.interaction is HoldInteraction);
                Weapon.GetComponent<Axe>().ChargeAttack();
            }
            else
            {
                //Debug.Log("regular attack");
               // Debug.Log(context.interaction is TapInteraction);
                Weapon.GetComponent<WeaponBase>().Attack();
            }
        }
        else if (context.canceled && context.interaction is HoldInteraction && Weapon.GetComponent<Axe>())
        {
            //Debug.Log(actuationTime);
            //Debug.Log("released");
            if (Weapon.GetComponent<Axe>() && Weapon.GetComponent<Axe>().GetCanCharge())
            {
                //Debug.Log(actuationTime);
                Weapon.GetComponent<Axe>().ReleaseCharge();
            }
        }
    }

    //Jump function
    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.ReadValueAsButton();
        jumped = context.action.triggered;
        //Debug.Log("Jumped");
    }

    //Sprint function: Sets isSprinting to true
    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
        isSprinting = context.action.triggered;

        if(context.performed)
        {
            //Debug.Log("is sprinting");
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
        //Debug.Log(controller.velocity);

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
                //Debug.Log("Not enough stamina");
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
            yield return staminaRegenTick;
        }
        staminaRegen = null;
    }
	
	private void UpdateHealth()
	{
        if (health < healthMax)
        {
            //Debug.Log("Updating health");
            healthBar.value = health;
            if (healthRegen != null)
            {
                StopCoroutine(healthRegen);
            }
            healthRegen = StartCoroutine(RegenHealth());
        }
    }
	
	private IEnumerator RegenHealth()
	{
		while(health < healthMax)
		{
			health += healthRegenAmount;//variable needs testing
			healthBar.value = health;
			yield return healthRegenTick;
		}
		healthRegen = null;
	}

    //Subtracts damage from player health
    public void TakeDamage(float damage, EnemyBase enemy)
    {
        health -= damage;
		healthBar.value = health;
        //Debug.Log(health);
        if(hasThorns)
        {
            enemy.TakeDamage(enemy.enemyData.stats.damage * (reflectPercentage * 0.01f));
        }
    }

	public void AddHealth(float heal)
	{
		health+=heal;
		if(health>healthMax)
			health = healthMax;
		healthBar.value = health;
		Debug.Log(health);
	}

    public void AddUpgrade(UpgradeData ud)
    {
        upgrades.Add(ud);
    }


	
	private void OnDisable()
	{
		playerData.stats = orginialPlayerStats;
        for(int i = 0; i < upgrades.Count; i++)
        {
            if(upgrades[i].unique)
            {
                if(upgrades[i].upgradeType == UpgradeType.Special)
                    playerData.specialUpgrades.Add(upgrades[i]);
                if (upgrades[i].upgradeType == UpgradeType.Weapon)
                    GetWeapon().weaponData.upgrades.Add(upgrades[i]);
            }
        }
	}

    //Setter / Modifier functions

    public void ActivateCrit(float multi, float chance)
    {
        hasCrit = true;
        critMultiplier = multi;
        critChance = chance;
    }

    public void ActivateThorns(float reflect)
    {
        hasThorns = true;
        reflectPercentage = reflect;
    }

    public void SetWeapon(GameObject weapon)
    {
        Weapon = weapon;
    }

    public void StoreOrginialData(PlayerData pd)
    {
        playerData = pd;

        orginialPlayerStats = new PlayerStats(pd.stats.health, pd.stats.stamina, pd.stats.speed);
    }

    public void SetData(PlayerData pd)
    {
        playerData = pd;

        playerStats = new PlayerStats(pd.stats.health, pd.stats.stamina, pd.stats.speed);
    }

    public void IncreaseHealthRegen(float amount)
    {
        healthRegenAmount += amount;
    }

    //Getter functions

    public WeaponBase GetWeapon()
    {
        return Weapon.GetComponent<WeaponBase>();
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public bool GetHasCrit()
    {
        return hasCrit;
    }

    public float GetCritMultiplier()
    {
        return critMultiplier;
    }

    public float GetCritChance()
    {
        return critChance;
    }
}
