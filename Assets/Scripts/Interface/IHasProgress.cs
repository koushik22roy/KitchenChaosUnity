using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    public event Action<OnProgressChangedEvent> OnProgressChanged;
    public class OnProgressChangedEvent
    {
        public float progressNormalized;
    }
}
