using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter
{
    public Action OnCut;
    public Action<OnProgressChangedEvent> OnProgressChanged;
    public class OnProgressChangedEvent {
        public float progressNormalized;
    }


    [SerializeField] CuttingReciepeSO[] cutKitchenObjectSO;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //ther is no kitchen object is here 
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingReciepeSO cuttingReciepeSO = GetCuttingReciepeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(new OnProgressChangedEvent
                    {
                        progressNormalized = (float)cuttingProgress / cuttingReciepeSO.cuttingProgressMax
                    });
                }
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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //cut the kitchen Object 
            cuttingProgress++;
            OnCut?.Invoke();

            CuttingReciepeSO cuttingReciepeSO = GetCuttingReciepeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(new OnProgressChangedEvent
            {
                progressNormalized = (float)cuttingProgress / cuttingReciepeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingReciepeSO.cuttingProgressMax)
            {
                KitchenObjectScriptableObject outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private KitchenObjectScriptableObject GetOutputForInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {

        CuttingReciepeSO cuttingReciepeSO = GetCuttingReciepeSOWithInput(inputKitchenObjectSO);
        if (cuttingReciepeSO != null)
        {
            return cuttingReciepeSO.output;
        }
        else
        {
            return null;
        }
    }

    private bool HasRecipeWithInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        CuttingReciepeSO cuttingReciepeSO = GetCuttingReciepeSOWithInput(inputKitchenObjectSO);
        return cuttingReciepeSO != null;
    }

    private CuttingReciepeSO GetCuttingReciepeSOWithInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        foreach (CuttingReciepeSO cuttingReciepeSO in cutKitchenObjectSO)
        {
            if (cuttingReciepeSO.input == inputKitchenObjectSO)
            {
                return cuttingReciepeSO;
            }
        }
        return null;
    }
}
