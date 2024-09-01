using UnityEngine;
using System;

public class CuttingCounter : BaseCounter,IHasProgress
{
    public static event EventHandler OnAnyCut;
      
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public Action OnCut;
    public event Action<IHasProgress.OnProgressChangedEvent> OnProgressChanged;

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

                    OnProgressChanged?.Invoke(new IHasProgress.OnProgressChangedEvent
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
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
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


            OnAnyCut?.Invoke(this,EventArgs.Empty);

            CuttingReciepeSO cuttingReciepeSO = GetCuttingReciepeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(new IHasProgress.OnProgressChangedEvent
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
