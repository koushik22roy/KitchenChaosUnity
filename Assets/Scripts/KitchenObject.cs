using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectScriptableObject kitchenObjectSO;
    public KitchenObjectScriptableObject GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
