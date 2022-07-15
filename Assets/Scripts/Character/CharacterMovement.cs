using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private DieConfiguration dieConfiguration;
    [SerializeField] private float movementCooldown;

    private GameInputs.MovementActions _movementActions;
    private float _currentMovementCooldown = 0f;
    private DieState _dieState;
    private EffectStateManager _effectStateManager;

    void Awake()
    {
        _effectStateManager = FindObjectOfType<EffectStateManager>();
    }

    void Start()
    {
        _dieState = new DieState(dieConfiguration);
        
        _movementActions = new GameInputs().Movement;
        _movementActions.Enable();
    }

    void Update()
    {
        MoveDirection moveDirection = GetDirection();
        MoveDie(moveDirection);
    }
    
    private MoveDirection GetDirection()
    {
        Vector2 movement = _movementActions.Movement.ReadValue<Vector2>();
        if (movement.x == 1)
        {
            return MoveDirection.Right;
        }

        if (movement.x == -1)
        {
            return MoveDirection.Left;
        }

        if (movement.y == 1)
        {
            return MoveDirection.Up;
        }

        if (movement.y == -1)
        {
            return MoveDirection.Down;
        }

        return MoveDirection.None;
    }
    
    private void MoveDie(MoveDirection moveDirection)
    {
        if (_currentMovementCooldown > 0f) return;
        switch (moveDirection)
        {
            case MoveDirection.Left:
                transform.Translate(Vector2.left);
                break;
            case MoveDirection.Up:
                transform.Translate(Vector2.up);
                break;
            case MoveDirection.Right:
                transform.Translate(Vector2.right);
                break;
            case MoveDirection.Down:
                transform.Translate(Vector2.down);
                break;
            case MoveDirection.None:
                return;
        }
        
        ChangeDieState(moveDirection);
        StartCoroutine(ColldownMovement());
    }

    IEnumerator ColldownMovement()
    {
        _currentMovementCooldown = movementCooldown;
        while (_currentMovementCooldown > 0f)
        {
            _currentMovementCooldown -= Time.deltaTime;
            yield return null;
        }

        _currentMovementCooldown = 0f;
    }
    
    private void ChangeDieState(MoveDirection moveDirection)
    {
        _dieState.ChangeState(moveDirection);
        _effectStateManager.ChangeEffect(_dieState.FaceCentral);
    }
}