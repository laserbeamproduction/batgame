using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
    private const string typeName = "BatCompanyOnline";
    private const string gameName = "endlessMode";
    private HostData[] hostList;
    public GameObject playerPrefab;

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
                if (GUI.Button(new Rect(10, 300 + (110 * i), 100, 50), hostList[i].gameName))
                    JoinServer(hostList[i]);
            }
        }
    }

    private void StartServer() {
        Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
        SpawnPlayerOne();
        EventManager.TriggerEvent(EventTypes.INSTANTIATE_OBJECT_POOL); //Sent Event to start spawning objectpool
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
        SpawnPlayerTwo();
    }

    void SpawnPlayerOne() {
        Network.Instantiate(playerPrefab, new Vector3(1f, -3f, 0), Quaternion.identity, 0);
    }

    void SpawnPlayerTwo() {
        Network.Instantiate(playerPrefab, new Vector3(-1f, -3f, 0), Quaternion.identity, 0);
    }
}
