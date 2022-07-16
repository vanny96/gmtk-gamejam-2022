using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndTickBehaviour : MonoBehaviour
{
    private ClockManager _clockManager;
    
    void Awake()
    {
        _clockManager = FindObjectOfType<ClockManager>();
    }

    private void Start()
    {
        StartCoroutine(EndBackgrundTick());
    }

    void Update()
    {
        
    }

    private IEnumerator EndBackgrundTick()
    {
        while (true)
        {
            _clockManager.Tick();
            yield return new WaitForSeconds(1f);
        }
    }
}
