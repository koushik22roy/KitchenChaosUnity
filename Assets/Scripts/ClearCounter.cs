using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectSO;


// Interact with items
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //ther is no kitchen object is here 
            if (player.HasKitchenObject()){
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {

            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
