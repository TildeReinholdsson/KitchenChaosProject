using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else
            {
                //player is not carrying anything
            }
        } 
        else 
        {
            //there is a kitchen object here
            if (player.HasKitchenObject()) 
            {
                //player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) 
                {
                    //player is holding plate
                    if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                } 
                else 
                {
                        //player is not holding plate but something else
                        if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) 
                        {
                            //counter is holding plate
                            if (plateKitchenObject.TryAddIngridient(player.GetKitchenObject().GetKitchenObjectSO())) {
                                player.GetKitchenObject().DestroySelf();
                            }
                        }
                }
            } else {
                //player is not  carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
  
}
