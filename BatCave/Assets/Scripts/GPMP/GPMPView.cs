using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GPMPView : MonoBehaviour {

    GooglePlayHelper gph;
    public Text statusLog;

    // Use this for initialization
    void Start () {
        GooglePlayHelper gph = GooglePlayHelper.GetInstance();
    }

    public void Login() {
        GooglePlayHelper.GetInstance().Login();
    }

    public void StartMatchMaking() {
        GPMPController.GetInstance().StartMatchMaking();
    }

    public void StartWithInvites() {
        GPMPController.GetInstance().StartWithInvitation();
    }

    public void LeaveCurrentRoom() {
        GPMPController.GetInstance().LeaveRoom();
    }

    void Update() {
        statusLog.text = GPMPController.GetInstance().messagelog;
    }

}
