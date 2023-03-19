using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabedObject += ContainerCounter_OnPlayerGrabedObject;
    }

    private void ContainerCounter_OnPlayerGrabedObject()
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
