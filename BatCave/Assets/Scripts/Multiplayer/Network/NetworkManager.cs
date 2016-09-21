using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
    private const string typeName = "BatCompanyOnline";
    private const string gameName = "endlessMode";
    private HostData[] hostList;

    public void PlayOnlineClicked() {
        if (!Network.isClient && !Network.isServer) {
            RefreshHostList();
        }

        StartCoroutine(WaitForList());
    }

    IEnumerator WaitForList() {
        yield return new WaitForSeconds(5);

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

            if ((i + 1) == hostList.Length) {
                StartServer();
                return;
            }
        }
    }

    public void HostServerClicked() {
        StartServer();
    }

    public void RefreshServerClicked()
    {
        RefreshHostList();
    }

    void OnGUI(){
        if (hostList != null)
        {
            for (int i = 0; i < hostList.Length; i++)
            {
                if (GUI.Button(new Rect(0, 0 + (0 * i), 0, 0), hostList[i].gameName))
                    JoinServer(hostList[i]);
            }
        }
    }

    private void StartServer() {
        Network.InitializeServer(1, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
        EventManager.TriggerEvent(EventTypes.SERVER_STARTED); //Sent Event to start spawning objectpool
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }

    private void RefreshHostList()
    {
        MasterServer.RequestHostList(typeName);
    }

    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        Debug.Log("Server Joined");
        EventManager.TriggerEvent(EventTypes.PLAYER_TWO_JOINED);
    }
}
