using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Util;
using Util.ExtensionMethods;
using static Util.DieEffect;

namespace Character
{
    public class CharacterBehaviour : MonoBehaviour, IKillable
    {
        [SerializeField] private DieConfiguration dieConfiguration;
        [SerializeField] private float movementCooldown;
        [SerializeField] private RectTransform gameoverPanel;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private new ParticleSystem particleSystem;
    
        private EffectStateManager _effectStateManager;
        private ClockManager _clockManager;
        private AudioManager _audioManager;

        private RectTransform _dieFullViewPanel;
        private GameInputs.MovementActions _movementActions;
        private float _currentMovementCooldown = 0f;
        private DieState _dieState;
        private bool _canMove = false;

        #region Starting
        void Awake()
        {
            _effectStateManager = FindObjectOfType<EffectStateManager>();
            _clockManager = FindObjectOfType<ClockManager>();
            _audioManager = FindObjectOfType<AudioManager>();
            _dieFullViewPanel = GameObject.Find("DieFullView").GetComponent<RectTransform>();
        }

        void Start()
        {
            _dieState = new DieState(dieConfiguration);
            ChangeDieState(MoveDirection.None); 
            RegisterMovement();
        }

        private void RegisterMovement()
        {
            _movementActions = new GameInputs().Movement;
            _movementActions.Movement.Enable();
            _movementActions.Movement.started += OnMovementInput;
            StartCoroutine(StartMoving());
        }

        private IEnumerator StartMoving()
        {
            yield return null;
            _canMove = true;
        }

        #endregion
        #region Listeners
        void OnDestroy()
        {
            _movementActions.Disable();
        }
        
        public void Kill()
        {
            _canMove = false;
            gameoverPanel.gameObject.SetActive(true);
        }
        
        
        #endregion
        #region Movement
        private void OnMovementInput(InputAction.CallbackContext context)
        {
            if(!_canMove || Time.frameCount == 1) return;
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
                _audioManager.Play(AudioManager.Audio.DieMovement);
                particleSystem.Play();
                
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
        #endregion
        #region Die state
        private void ChangeDieState(MoveDirection moveDirection)
        {
            _dieState.ChangeState(moveDirection);
            _effectStateManager.ChangeEffect(_dieState.FaceCentral);
            UpdateSprite();
            UpdateUI();
        }

        private void UpdateSprite()
        {
            spriteRenderer.sprite = ParseSprite(_dieState.FaceCentral);
        }
        
        private void UpdateUI()
        {
            _dieFullViewPanel.Find("Up").GetComponent<Image>().sprite = ParseSprite(_dieState.FaceUp);
            _dieFullViewPanel.Find("Center").GetComponent<Image>().sprite = ParseSprite(_dieState.FaceCentral);
            _dieFullViewPanel.Find("Down").GetComponent<Image>().sprite = ParseSprite(_dieState.FaceDown);
            _dieFullViewPanel.Find("DoubleDown").GetComponent<Image>().sprite = ParseSprite(_dieState.FaceDoubleDown);
            _dieFullViewPanel.Find("Left").GetComponent<Image>().sprite = ParseSprite(_dieState.FaceLeft);
            _dieFullViewPanel.Find("Right").GetComponent<Image>().sprite = ParseSprite(_dieState.FaceRight);
        }

        private Sprite ParseSprite(DieEffect dieEffect)
        {
            return dieEffect switch
            {
                None => sprites[0],
                Electricity => sprites[3],
                Ice => sprites[1],
                Fire => sprites[2],
                Wind => sprites[0],
                Earth => sprites[0],
                DieEffect.Light => sprites[6],
                Darkness => sprites[7],
                _ => throw new ArgumentOutOfRangeException(nameof(dieEffect), dieEffect, null)
            };
        }
        #endregion
    }
}