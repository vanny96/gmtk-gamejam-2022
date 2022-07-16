using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;
using Util.ExtensionMethods;

namespace Character
{
    public class CharacterBehaviour : MonoBehaviour, IKillable
    {
        [SerializeField] private DieConfiguration dieConfiguration;
        [SerializeField] private float movementCooldown;
        [SerializeField] private RectTransform gameoverPanel;
    
        private EffectStateManager _effectStateManager;
        private ClockManager _clockManager;

        private GameInputs.MovementActions _movementActions;
        private float _currentMovementCooldown = 0f;
        private DieState _dieState;
        private bool alive = true;

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
            if(!alive) return;
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
            Vector2 movement = moveDirection.GetVector();

            if (ValidMovement(movement))
            {
                transform.Translate(movement);

                _clockManager.Tick();
                ChangeDieState(moveDirection);
                StartCoroutine(ColldownMovement());
            }
        }

        private bool ValidMovement(Vector2 movement)
        {
            RaycastHit2D hitcheck = Physics2D.Raycast(transform.position, movement, movement.magnitude);
            if (hitcheck.collider != null)
            {
                if (hitcheck.collider.CompareTag("Wall"))
                    return false;

                if (hitcheck.collider.CompareTag("Enemy"))
                {
                    Kill();
                    return false;
                }
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

        public void Kill()
        {
            alive = false;
            gameoverPanel.gameObject.SetActive(true);
        }
    }
}