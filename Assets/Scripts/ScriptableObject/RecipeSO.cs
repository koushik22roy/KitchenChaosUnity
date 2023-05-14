using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectScriptableObject> kitchenObjectsSOList;
    public string recipeName;
}
