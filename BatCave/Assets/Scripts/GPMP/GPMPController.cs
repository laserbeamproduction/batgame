using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class GPMPController : MonoBehaviour, RealTimeMultiplayerListener {

    void Start() {
        EventManager.StartListening(GPMPEvents.Types.GPMP_CANCEL_MATCH_MAKING.ToString(), OnMatchMakingCanceled);
        EventManager.StartListening(GPMPEvents.Types.GPMP_LEAVE_GAME.ToString(), OnPlayerLeavesTheGame); 
        EventManager.StartListening(GPMPEvents.Types.GPMP_VIEW_INVITES.ToString(), OnViewInvites);
        EventManager.StartListening(GPMPEvents.Types.GPMP_SEARCH_QUICK_MATCH.ToString(), OnStartSearchForQuickMatch);
        EventManager.StartListening(GPMPEvents.Types.GPMP_START_WITH_INVITE.ToString(), OnStartInvitingForMatch);
    }

    void OnDestroy() {
        EventManager.StopListening(GPMPEvents.Types.GPMP_CANCEL_MATCH_MAKING.ToString(), OnMatchMakingCanceled);
        EventManager.StopListening(GPMPEvents.Types.GPMP_LEAVE_GAME.ToString(), OnPlayerLeavesTheGame);
        EventManager.StopListening(GPMPEvents.Types.GPMP_VIEW_INVITES.ToString(), OnViewInvites);
        EventManager.StopListening(GPMPEvents.Types.GPMP_SEARCH_QUICK_MATCH.ToString(), OnStartSearchForQuickMatch);
        EventManager.StopListening(GPMPEvents.Types.GPMP_START_WITH_INVITE.ToString(), OnStartInvitingForMatch);
    }

    private static GPMPController instance;

    public static GPMPController GetInstance() {
        if (instance == null)
            instance = new GPMPController();
        return instance;
    }

    void OnViewInvites(object obj) {
        PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(this);
    }

    void OnPlayerLeavesTheGame(object arg0) {
        DebugMP.Log("Player is leaving the room");
        PlayGamesPlatform.Instance.RealTime.LeaveRoom();
    }
    
    public void OnMatchMakingCanceled(object obj) {
        DebugMP.Log("Player canceled match making");
        PlayGamesPlatform.Instance.RealTime.LeaveRoom();
    }

    private void OnStartSearchForQuickMatch(object model) {
        DebugMP.Log("Start searching for quick match");
        GPMPMatchModel matchModel = (GPMPMatchModel)model;
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(matchModel.minimumAmountOpponents, matchModel.maximumAmountOpponents, 0, this);
        LoadingController.LoadScene(LoadingController.Scenes.GPMP_WAITING_ROOM);
    }

    private void OnStartInvitingForMatch(object model) {
        DebugMP.Log("Start inviting for match");
        GPMPMatchModel matchModel = (GPMPMatchModel)model;
        PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(matchModel.minimumAmountOpponents, matchModel.maximumAmountOpponents, 0, this);
        LoadingController.LoadScene(LoadingController.Scenes.GPMP_WAITING_ROOM);
    }
    
    /*
     Functions handling on notification clicked for invitation
         */

    public void OnInvitationReceived(Invitation invitation, bool shouldAutoAccept) {
        DebugMP.Log("Invitation recieved from: " + invitation.Inviter.DisplayName);
        if (shouldAutoAccept) {
            LoadingController.LoadScene(LoadingController.Scenes.GPMP_WAITING_ROOM);
            PlayGamesPlatform.Instance.RealTime.AcceptInvitation(invitation.InvitationId, this);
        }
    }


    /*
     RealTimeMultiplayerListener functions
         */

    public void OnRoomSetupProgress(float percentage) {
        DebugMP.Log("Room setup progress: " + percentage);
        EventManager.TriggerEvent(GPMPEvents.Types.GPMP_REPORT_ROOM_SETUP_PROGRESS.ToString(), percentage);
    }

    public void OnRoomConnected(bool success) {
        if (success) {
            DebugMP.Log("Room connected. Start loading game scene");
            EventManager.TriggerEvent(GPMPEvents.Types.GPMP_MATCH_MAKING_DONE.ToString());

            // Reset save model
            SaveLoadController.GetInstance().GetMultiplayerSession().Reset();

            // Start versusn screen
            LoadingController.LoadScene(LoadingController.Scenes.GPMP_VERSUS_SCREEN);
        } else {
            DebugMP.Log("On Room Connected status: " + success);
            PlayGamesPlatform.Instance.RealTime.LeaveRoom();
            LoadingController.LoadScene(LoadingController.Scenes.GPMP_LOBBY);
        }
    }

    public void OnLeftRoom() {
        DebugMP.Log("Player left the room");
        LoadingController.LoadScene(LoadingController.Scenes.GPMP_LOBBY);
    }

    public void OnParticipantLeft(Participant participant) {
        DebugMP.Log("Player " + participant.DisplayName + " has left the room");
    }

    public void OnPeersConnected(string[] participantIds) {
        foreach (string participantID in participantIds) {
            DebugMP.Log("Player " + participantID + " has joined the room");
        }
    }

    public void OnPeersDisconnected(string[] participantIds) {
        foreach (string participantID in participantIds) {
            DebugMP.Log("Player " + participantID + " has left the room");
        }
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
        EventManager.TriggerEvent(GPMPEvents.Types.GPMP_MESSAGE_RECIEVED.ToString(), data);
       // DebugMP.Log("We have received some gameplay messages from participant ID:" + senderId);
    }
}
