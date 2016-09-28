using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour {
    public GameObject confirmPopUp;
    public GameObject[] storeItems;
    public Button[] storeButtons;
    public Text coins;
    
    private int coinAmountFromSave;
    private int currentCoinAmount;

    private void Start() {
        //Add listeners
        EventManager.StartListening(EventTypes.PURCHASE_STARTED, PurchaseStarted);
        EventManager.StartListening(EventTypes.PURCHASE_CONFIRMED, PurchaseConfirmed);
        EventManager.StartListening(EventTypes.PURCHASE_CANCELED, PurchaseCanceled);

        //Disable confirm purchase popup
        confirmPopUp.SetActive(false);

        //Show amount of coins from savegame
        coinAmountFromSave = SaveLoadController.GetInstance().GetPlayer().GetTotalCoins();
        currentCoinAmount = coinAmountFromSave;
        coins.text = "Coins: " + coinAmountFromSave.ToString();
    }

    private void OnDestroy() {
        EventManager.StopListening(EventTypes.PURCHASE_STARTED, PurchaseStarted);
        EventManager.StopListening(EventTypes.PURCHASE_CONFIRMED, PurchaseConfirmed);
        EventManager.StopListening(EventTypes.PURCHASE_CANCELED, PurchaseCanceled);
    }

    private void PurchaseStarted(object item) {
        //Check price and current amount
        //if (currentCoinAmount >= item.price) {
            confirmPopUp.SetActive(false);
        //}
    }

    private void PurchaseConfirmed(object item) {
        //Confirm purchase and adjust coin amount accordigly
    }

    private void PurchaseCanceled(object value) {
        //Return to store
    }

    public void PurchaseItem(GameObject item) {

    }
}
