using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/KitchenObjectSO")]
public class KitchenObjectScriptableObject : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
