using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    public Action OnPlayerGrabedObject; 

    [SerializeField] private KitchenObjectScriptableObject kitchenObjectSO;

    // Interact with items
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabedObject?.Invoke();
        }
    }
}
