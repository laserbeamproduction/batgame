using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour {
    private const string typeName = "BatCompanyOnline";
    private const string gameName = "endlessMode";
    private HostData[] hostList;

    private bool serverStarted = false;
    private bool refreshedList = false;
    private bool restartedSearch = false;
    private float timer;

    void Start() {
        EventManager.StartListening(EventTypes.RESTART_SEARCH, RestartSearch);
    }

    //When Player clicks Play Online button
    void RestartSearch(object value) {
        PlayOnlineClicked();
    }

    public void PlayOnlineClicked() {
        EventManager.TriggerEvent(EventTypes.PLAY_ONLINE_PRESSED);

        if (!Network.isClient && !Network.isServer) {
            RefreshHostList();
        }
        StartCoroutine(WaitForList());
    }

    //Simple Matchmaking
    IEnumerator WaitForList() {
        yield return new WaitForSeconds(1f);

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
        serverStarted = true;
    }

    void FixedUpdate() {
        if (serverStarted) {
            timer += Time.deltaTime;

            if (timer > UnityEngine.Random.Range(2f, 8f) && !refreshedList) {
                RefreshHostList();
                refreshedList = true;
                Debug.Log("Refreshing Host List");
            }

            if (timer > UnityEngine.Random.Range(8f, 15f) && !restartedSearch) {
                KeepLookingForServers();
                restartedSearch = true;
                Debug.Log("Starting Search");
            }
        }
    }

    void KeepLookingForServers() {
        if (hostList.Length == 2)
        {
            PlayerPrefs.SetString("FailedToFindPlayer", "true");
            Network.Disconnect();
        }
        else {
            timer = 0;
            restartedSearch = false;
            refreshedList = false;
        }
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
        PlayerPrefs.SetString("FailedToFindPlayer", "false");
        serverStarted = false;
        Network.Connect(hostData);
    }

    //Player Connected to server - Time to launch the game.
    void OnConnectedToServer()
    {
        PlayerPrefs.SetString("FailedToFindPlayer", "false");
        serverStarted = false;
        Debug.Log("Server Joined");
        EventManager.TriggerEvent(EventTypes.PLAYER_TWO_JOINED);
    }

    void OnPlayerConnected(NetworkPlayer player) {
        PlayerPrefs.SetString("FailedToFindPlayer", "false");
        serverStarted = false;
    }

    //Throw Error when connection failed & Restart search for game
    void OnFailedToConnect(NetworkConnectionError error) {
        Debug.Log("Connection Failed: " + error + " //Starting Search Again");
        PlayOnlineClicked();
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("clean up after " + player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void OnDisconnectedFromServer(NetworkDisconnection info) {
        Debug.Log("Disconnected the server: " + info + " //Reloading Scene");
        serverStarted = false;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
