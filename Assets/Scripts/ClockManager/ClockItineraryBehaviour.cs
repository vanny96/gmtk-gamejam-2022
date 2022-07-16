using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Util.Clock;
using Util.ExtensionMethods;

public class ClockItineraryBehaviour : MonoBehaviour, IClockBehaviour
{
    [SerializeField] private List<MoveDirection> movements;
    [SerializeField] private bool reverses;
    [HideInInspector] public bool active;
    
    private int _currMovement = 0;
    private bool _forward = true;

    void Awake()
    {
        active = true;
    }

    private void Start()
    {
        FindObjectOfType<ClockManager>().RegisterObserver(this);
    }
    
    private void OnDestroy()
    {
        FindObjectOfType<ClockManager>()?.UnRegisterObserver(this);
    }

    public void OnClockTick()
    {
        if(!active) return;
        int forward = _forward ? 1 : -1;
        _currMovement += forward;

        Vector2 movement = movements[_currMovement].GetVector() * forward;
        if(ValidMovement(movement))
            transform.position += (Vector3) movement;

        bool reachedEnd = _currMovement == movements.Count - 1 && _forward;
        if (reachedEnd || _currMovement == 0)
        {
            if (reverses)
            {
                _currMovement++;
                _forward = !_forward;
            }
            else
            {
                _currMovement = 0;
            }
        }
    }
    
    private bool ValidMovement(Vector2 movement)
    {
        RaycastHit2D[] hitchecks = Physics2D.RaycastAll(transform.position, movement, movement.magnitude);
        bool hitsWall = hitchecks.Any(hitcheck => hitcheck.collider.CompareTag("Wall"));

        return !hitsWall;
    }
}
