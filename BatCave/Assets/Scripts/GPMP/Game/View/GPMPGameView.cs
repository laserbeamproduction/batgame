using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;

public class GPMPGameView : MonoBehaviour {

    public void LeaveGame() {
        EventManager.TriggerEvent(GPMPEvents.Types.GPMP_LEAVE_GAME.ToString());
    }
}
