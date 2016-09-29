using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour {
    public GameObject confirmPopUp;
    public GameObject notEnoughFunds;
    public Button[] storeButtons;

    public Text coins;
    
    private int coinAmountFromSave;
    private int currentCoinAmount;
    private bool matchFound;

    //store current item being bought
    private StoreItemModel itemInCart;

    private void Start() {
        //Disable confirm purchase popup
        confirmPopUp.SetActive(false);
        notEnoughFunds.SetActive(false);
        itemInCart = null;

        //Show amount of coins from savegame
        coinAmountFromSave = SaveLoadController.GetInstance().GetPlayer().GetTotalCoins();
        currentCoinAmount = coinAmountFromSave + 100;
        coins.text = "Coins: " + currentCoinAmount.ToString();
    }

    private void PurchaseStarted(GameObject item) {
        //Check price and current amount
        StoreItemModel itemToPurchase = item.GetComponent<StoreItemModel>();
        if (itemToPurchase.goldPrice <= currentCoinAmount)
        {
            itemInCart = itemToPurchase; //save item player wants to purchase (Cart)
            confirmPopUp.SetActive(true);
        }
        else {
            notEnoughFunds.SetActive(true);
        }
    }

    public void PurchaseConfirmed() {
        //Confirm purchase and adjust coin amount accordigly
        SaveLoadController.GetInstance().GetPlayer().AddUnlockedItem(itemInCart.itemID);
        GooglePlayHelper.GetInstance().SaveGame();
        currentCoinAmount -= itemInCart.goldPrice;
        coins.text = "Coins: " + currentCoinAmount.ToString();
        confirmPopUp.SetActive(false);
        EventManager.TriggerEvent(EventTypes.PURCHASE_CONFIRMED);
        itemInCart = null;
    }

    public void PurchaseCanceled() {
        //Return to store
        confirmPopUp.SetActive(false);
        notEnoughFunds.SetActive(false);
        itemInCart = null;
    }

    public void PurchaseItem(GameObject item) {
        //Quick hack to set a skin active
        foreach (int id in SaveLoadController.GetInstance().GetPlayer().GetUnlockedItems()) {
            if (item.GetComponent<StoreItemModel>().itemID == id)
            {
                SetSkinActive(item.GetComponent<StoreItemModel>().itemID);
                Debug.Log("Skin: " + item.GetComponent<StoreItemModel>().itemID + " is active");
                matchFound = true;
            }
        }

        if (!matchFound) {
            PurchaseStarted(item);
            Debug.Log("Purchase: " + item.GetComponent<StoreItemModel>().itemID + " started");
        }

        matchFound = false;
    }

    private void SetSkinActive(int id) {
        SaveLoadController.GetInstance().GetPlayer().SetActiveSkinID(id);
        GooglePlayHelper.GetInstance().SaveGame();
        EventManager.TriggerEvent(EventTypes.NEW_SKIN_ACTIVE);
    }
}
