using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class InputManager : MonoBehaviour
{
    public static Vector2 movement;


    private PlayerInput playerInput;
    private InputAction moveAction;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];

    }

    void Update()
    {
        movement = moveAction.ReadValue<Vector2>();
    }


}
