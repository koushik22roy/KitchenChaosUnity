using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DeliveryManager : MonoBehaviour
{
    public Action OnRecipeSpawned;
    public Action OnRecipeCompleted;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingMaxRecipeMax = 4;


    private void Awake()
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();

    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
         if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingMaxRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke();
            }
            
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectsSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
               
                // has same number of ingredients
                bool plateContentMatchesRecipe = true;

                foreach(KitchenObjectScriptableObject recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectsSOList)
                {
                    //cycle through all the recipe
                    bool ingredientFound = false;

                    foreach(KitchenObjectScriptableObject plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycle through all the plates
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateContentMatchesRecipe = false;
                    }
                }


                if (plateContentMatchesRecipe)
                {
                    Debug.Log("player delivered the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke();
                    return; 
                }
            }
        }
        Debug.Log("Not a correct recipe");
    }

    public List<RecipeSO> GetWaitRecipeSOList()
    {
        return waitingRecipeSOList;
    }

}
