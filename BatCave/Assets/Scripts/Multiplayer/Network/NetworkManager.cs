using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
    private const string typeName = "BatCompanyOnline";
    private const string gameName = "endlessMode";
    private HostData[] hostList;

    //When Player clicks Play Online button
    public void PlayOnlineClicked() {
        if (!Network.isClient && !Network.isServer) {
            RefreshHostList();
        }

        StartCoroutine(WaitForList());
    }

    //Simple Matchmaking
    IEnumerator WaitForList() {
        yield return new WaitForSeconds(3);

        if (hostList != null && hostList.Length >= 1)
        {
            StartSearch();
        }
        else {
            StartServer();
        }

        StopCoroutine(WaitForList());
    }

    private void StartSearch() {
        for (int i = 0; hostList.Length > i; i++)
        {
            if (hostList[i].connectedPlayers < hostList[i].playerLimit)
            {
                JoinServer(hostList[i]);
                return;
            }
        }

        StartServer();
    }

    //Start the server & Register to Hostlist
    private void StartServer() {
        Network.InitializeServer(1, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
        EventManager.TriggerEvent(EventTypes.SERVER_STARTED);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }

    //Get HostList from Masterserver
    private void RefreshHostList()
    {
        MasterServer.RequestHostList(typeName);
    }

    //Join Selected server
    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    //Player Connected to server - Time to launch the game.
    void OnConnectedToServer()
    {
        Debug.Log("Server Joined");
        EventManager.TriggerEvent(EventTypes.PLAYER_TWO_JOINED);
    }

    //Throw Error when connection failed & Restart search for game
    void OnFailedToConnect(NetworkConnectionError error) {
        Debug.Log("Connection Failed: " + error + " //Starting Search Again");
        PlayOnlineClicked();
    }
}
