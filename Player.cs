using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private AgentAnimations agentAnimations;
    private AgentMover agentMover;

    private WeaponParent weaponParent;

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;

    private Vector2 pointerInput, movementInput;

    private void Update()
    {
        pointerInput = GetPointerInput();
        movementInput = movement.action.ReadValue<Vector2>().normalized;

        agentMover.MovementInput = movementInput;
        weaponParent.PointerPosition = pointerInput;
        AnimateCharacter();
    }

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        weaponParent.Attack();
    }

    private void Awake()
    {
        agentAnimations = GetComponentInChildren<AgentAnimations>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        agentMover = GetComponent<AgentMover>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        agentAnimations.RotateToPointer(lookDirection);
        agentAnimations.PlayAnimation(movementInput);
    }

    

}
