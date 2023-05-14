using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject
{
    public event Action<OnIngredientEventArgs> OnIngredientAdded;

    public class OnIngredientEventArgs : EventArgs
    {
        public KitchenObjectScriptableObject kitchenObjectSO;
    }


    [Header("Valid")]
    [SerializeField] private List<KitchenObjectScriptableObject> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectScriptableObject>();
    }

    public bool TryAddIngredient(KitchenObjectScriptableObject kitchenObjectSO)
    {
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //already has this type
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(new OnIngredientEventArgs { kitchenObjectSO = kitchenObjectSO });
            
        }
        return true;
    }

    public List<KitchenObjectScriptableObject> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
