using System.Collections.Generic;
using UnityEngine;
using Util.Clock;

public class ClockItineraryBehaviour : MonoBehaviour, IClockBehaviour
{
    [SerializeField] private List<Vector2> positions;
    [SerializeField] private bool reverses;
    
    private int _currPosition = 0;
    private bool _forward = true;

    private void Start()
    {
        FindObjectOfType<ClockManager>().RegisterObserver(this);
    }

    public void OnClockTick()
    {
        _currPosition += _forward ? 1 : -1;
        transform.position = positions[_currPosition];
        if (_currPosition == positions.Count - 1 || _currPosition == 0)
        {
            if (reverses)
            {
                _forward = !_forward;
            }
            else
            {
                _currPosition = 0;
            }
        }
    }
}
