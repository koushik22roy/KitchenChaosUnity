using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectScriptableObject kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjectList;

    private void Start()
    {
        //System.Action<PlateKitchenObject.OnIngredientEventArgs> plateKitchenObject_OnIngredientAdded = PlateKitchenObject_OnIngredientAdded;
        plateKitchenObject.OnIngredientAdded += plateKitchenObject_OnIngredientAdded;
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSO_GameObjectList)
        {
                kitchenObjectSOGameObject.gameObject.SetActive(false);
        }

    }

    private void plateKitchenObject_OnIngredientAdded(PlateKitchenObject.OnIngredientEventArgs obj)
    {
        foreach(KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSO_GameObjectList)
        {
            if(kitchenObjectSOGameObject.kitchenObjectSO == obj.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
