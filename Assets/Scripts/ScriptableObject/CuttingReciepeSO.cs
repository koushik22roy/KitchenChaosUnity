using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CuttingRecipeSO")]
public class CuttingReciepeSO : ScriptableObject
{
    public KitchenObjectScriptableObject input;
    public KitchenObjectScriptableObject output;
    public int cuttingProgressMax; 
}
