using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter,IHasProgress
{
    public event Action<IHasProgress.OnProgressChangedEvent> OnProgressChanged;

    public Action<STATE> OnStateChanged;

    public enum STATE
    {
        IDLE,
        FRYING,
        FRIED,
        BURNED,
    }

    private STATE state;

    [SerializeField] private FryingReciepeSO[] fryingReciepeSOArray;
    [SerializeField] private BurningReciepeSO[] burningReciepeSOArray;

    private FryingReciepeSO fryingReciepeSO;
    private float fryingTimer;

    private BurningReciepeSO burningReciepeSO;
    private float burningTImer;

    private void Start()
    {
        state = STATE.IDLE;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case STATE.IDLE:
                    break;

                case STATE.FRYING:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(new IHasProgress.OnProgressChangedEvent
                    {
                        progressNormalized = fryingTimer / fryingReciepeSO.fryingTimerMax
                    });

                    if (fryingTimer > fryingReciepeSO.fryingTimerMax)
                    {
                        //fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingReciepeSO.output, this);

                        state = STATE.FRIED;
                        burningTImer = 0f;

                        burningReciepeSO = GetBurningReciepeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(state);
                    }
                    break;

                case STATE.FRIED:
                    burningTImer += Time.deltaTime;

                    OnProgressChanged?.Invoke(new IHasProgress.OnProgressChangedEvent
                    {
                        progressNormalized = burningTImer / burningReciepeSO.burningTimerMax
                    });

                    if (burningTImer > burningReciepeSO.burningTimerMax)
                    {
                        //fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningReciepeSO.output, this);

                        state = STATE.BURNED;

                        OnStateChanged?.Invoke(state);

                        OnProgressChanged?.Invoke(new IHasProgress.OnProgressChangedEvent
                        {
                            progressNormalized = 0f 
                        });
                    }
                    break;

                case STATE.BURNED:
                    break;
            }



        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //ther is no kitchen object is here 
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carry something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingReciepeSO = GetFryingReciepeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = STATE.FRYING;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(state);

                    OnProgressChanged?.Invoke(new IHasProgress.OnProgressChangedEvent{
                        progressNormalized = fryingTimer / fryingReciepeSO.fryingTimerMax
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
            { //player is carrying something
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = STATE.IDLE;

                        OnStateChanged?.Invoke(state);

                        OnProgressChanged?.Invoke(new IHasProgress.OnProgressChangedEvent
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                //player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);

                state = STATE.IDLE;

                OnStateChanged?.Invoke(state);

                OnProgressChanged?.Invoke(new IHasProgress.OnProgressChangedEvent
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private KitchenObjectScriptableObject GetOutputForInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {

        FryingReciepeSO fryingReciepeSO = GetFryingReciepeSOWithInput(inputKitchenObjectSO);
        if (fryingReciepeSO != null)
        {
            return fryingReciepeSO.output;
        }
        else
        {
            return null;
        }
    }

    private bool HasRecipeWithInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        FryingReciepeSO fryingReciepeSO = GetFryingReciepeSOWithInput(inputKitchenObjectSO);
        return fryingReciepeSO != null;
    }

    private FryingReciepeSO GetFryingReciepeSOWithInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        foreach (FryingReciepeSO fryingRecipeSO in fryingReciepeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningReciepeSO GetBurningReciepeSOWithInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        foreach(BurningReciepeSO burningReciepeSO in burningReciepeSOArray)
        {
            if (burningReciepeSO.input == inputKitchenObjectSO)
            {
                return burningReciepeSO;
            }
        }
        return null;
    }
}
