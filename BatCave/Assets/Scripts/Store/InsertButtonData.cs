﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InsertButtonData : MonoBehaviour {
    public StoreItemModel storeItem;

    public Button storeButton;
    public Text itemTitle;
    public Text itemPrice;
    public GameObject pricePanel;

    private bool buttonReady;

    private bool newSkinActive;
    private int newActiveSkinID;

    private List<int> unlockedItems = new List<int>();
    public bool itemUnlocked;

    private void Start() {
        EventManager.StartListening(EventTypes.PURCHASE_CONFIRMED, ItemPurchased);
        EventManager.StartListening(EventTypes.NEW_SKIN_ACTIVE, NewSkinActive);
        buttonReady = false;
        newSkinActive = true;
    }

    private void ItemPurchased(object arg0) {
        buttonReady = false;
        Debug.Log("Refreshing Buttons");
    }

    private void NewSkinActive(object arg0) {
        newSkinActive = true;
    }

    private void FixedUpdate() {
        if (!buttonReady) {
            unlockedItems = SaveLoadController.GetInstance().GetPlayer().GetUnlockedItems();

            itemTitle.text = storeItem.skinName;
            itemPrice.text = storeItem.goldPrice.ToString();
            storeButton.GetComponent<Image>().sprite = storeItem.skinSprite;

            foreach (int item in unlockedItems)
            {
                if (item == storeItem.itemID)
                {
                    itemUnlocked = true;
                    pricePanel.SetActive(false);
                }
            }

            if (!itemUnlocked) {
                itemUnlocked = false;
                pricePanel.SetActive(true);
            }

            buttonReady = true;
        }

        if (newSkinActive) {
            newActiveSkinID = SaveLoadController.GetInstance().GetPlayer().GetActiveSkinID();
            if (newActiveSkinID == storeItem.itemID)
            {
                storeButton.interactable = false;
            }
            else {
                storeButton.interactable = true;
            }

            newSkinActive = false;
        }
    }
}
