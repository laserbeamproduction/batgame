using UnityEngine;
using System;
using GooglePlayGames;

public class GPMPOpponentView : MonoBehaviour {

    public float speed;

    private Rigidbody2D rigidbody;
    private Vector3 lastKnownPosition;
    public Vector3 playerOneSpawnpoint;
    public Vector3 playerTwoSpawnpoint;
    private GPMPMatchModel matchModel;

    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        EventManager.StartListening(GPMPEvents.Types.GPMP_UPDATE_OPPONENT_POSITION.ToString(), OnPositionUpdated);
        EventManager.StartListening(GPMPEvents.Types.GPMP_MATCH_INFO_READY.ToString(), OnMatchInfoReady);
        EventManager.StartListening(GPMPEvents.Types.GPMP_OPPONENT_DIED.ToString(), OnOpponentDied);

    }

    private void OnMatchInfoReady(object model) {
        matchModel = (GPMPMatchModel)model;
        if (matchModel.iAmTheHost) {
            lastKnownPosition = playerTwoSpawnpoint;
        } else {
            lastKnownPosition = playerOneSpawnpoint;
        }
    }

    void OnDestroy() {
        EventManager.StopListening(GPMPEvents.Types.GPMP_UPDATE_OPPONENT_POSITION.ToString(), OnPositionUpdated);
        EventManager.StopListening(GPMPEvents.Types.GPMP_MATCH_INFO_READY.ToString(), OnMatchInfoReady);
        EventManager.StopListening(GPMPEvents.Types.GPMP_OPPONENT_DIED.ToString(), OnOpponentDied);
    }

    private void OnOpponentDied(object arg0) {
        GetComponent<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnPositionUpdated(object data) {
        byte[] bytes = (byte[])data;
        float posX = BitConverter.ToSingle(bytes, 5);
        float posY = BitConverter.ToSingle(bytes, 9);
        float posZ = BitConverter.ToSingle(bytes, 13);
        lastKnownPosition = new Vector3(posX, posY, posZ);
    }
    
    void Update () {
        Vector3 pos = rigidbody.position;
        pos.x = Mathf.MoveTowards(pos.x, lastKnownPosition.x, speed * Time.deltaTime);
        rigidbody.position = pos;
        rigidbody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode2D.Force);

        //rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime);
        //rigidbody.position = lastKnownPosition;// Vector3.Lerp(transform.position, lastKnownPosition, Time.deltaTime * speed);
    }
}
