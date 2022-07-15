using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private DieConfiguration dieConfiguration;
    [SerializeField] private float movementCooldown;
    
    private EffectStateManager _effectStateManager;
    private ClockManager _clockManager;

    private GameInputs.MovementActions _movementActions;
    private float _currentMovementCooldown = 0f;
    private DieState _dieState;

    void Awake()
    {
        _effectStateManager = FindObjectOfType<EffectStateManager>();
        _clockManager = FindObjectOfType<ClockManager>();
    }

    void Start()
    {
        _dieState = new DieState(dieConfiguration);
        ChangeDieState(MoveDirection.None);
        
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
        int horizontal = (int) movement.x;
        int vertical = (int) movement.y;
        
        if (horizontal == 1)
            return MoveDirection.Right;

        if (horizontal == -1)
            return MoveDirection.Left;

        if (vertical == 1)
            return MoveDirection.Up;

        if (vertical == -1)
            return MoveDirection.Down;

        return MoveDirection.None;
    }
    
    private void MoveDie(MoveDirection moveDirection)
    {
        if (_currentMovementCooldown > 0f || moveDirection == MoveDirection.None) return;
        Vector2 movement = moveDirection switch
        {
            MoveDirection.Left => Vector2.left,
            MoveDirection.Up => Vector2.up,
            MoveDirection.Right => Vector2.right,
            MoveDirection.Down => Vector2.down,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (ValidMovement(movement))
        {
            transform.Translate(movement);

            ChangeDieState(moveDirection);
            StartCoroutine(ColldownMovement());
            _clockManager.Tick();
        }
    }

    private bool ValidMovement(Vector2 movement)
    {
        RaycastHit2D hitcheck = Physics2D.Raycast(transform.position, movement, movement.magnitude);
        if (hitcheck.collider != null)
        {
            if (hitcheck.collider.tag == "Wall")
                return false;
        }

        return true;
    }

    private void ChangeDieState(MoveDirection moveDirection)
    {
        _dieState.ChangeState(moveDirection);
        _effectStateManager.ChangeEffect(_dieState.FaceCentral);
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
}