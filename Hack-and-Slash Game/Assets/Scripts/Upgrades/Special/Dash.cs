using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : PlayerUpgradeBase
{
    [Header("References")]
    public Transform orientation;
    private CharacterController controller;
    private PlayerController pc;

    [Header("Dashing")]
    public float dashForce;
    public float dashSpeed;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    private bool canDash = false;

    [SerializeField] private InputActionReference actionReference;

    private void Start()
    {
        //controller = GetComponentInParent<CharacterController>();
        //pc = GetComponent<PlayerController>();
        //orientation = GetComponentInParent<Transform>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            canDash = true;
        }
        if (context.canceled)
        {
            canDash = false;
        }
    }

    public override void ApplyEffects(PlayerController player)
    {
        actionReference.action.performed += OnDash;
        controller = player.GetComponent<CharacterController>();
        pc = player;
        orientation = player.gameObject.transform;
    }

    void Update()
    {
        if (canDash && dashCdTimer <= 0)
        {
            DashAction();
        }
        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    public void DashAction()
    {
        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;

        controller.Move(forceToApply * Time.deltaTime * dashSpeed);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {
        canDash = false;
        dashCdTimer = dashCd;
    }

}
