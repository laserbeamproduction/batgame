using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
    public GameObject Echo;
    public Transform PlayerPos;
    public PlayerResources playerResources;
    public ScoreCalculator score;
    //public Light playerLight;
    //public Sprite batSprite;
    //public Sprite draculaSprite;
    //private SpriteRenderer spriteRenderer;

    //movement
    private Vector2 movement;
    public float speed;    

    //echo
    public float echoCoolDownTime;
    //public float sentCoolDownTime;
    public float currentCoolDownTime;
    private bool coolingDown;
    private Rigidbody2D rigidbody;
    
    //Shape Shift
    public bool isShapeShifted;
    public float batLightRange;
    //public float draculaLightRange;

	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        speed = SaveLoadController.GetInstance().GetOptions().GetControlSensitivity();
    }
	
	void Update () {
        //movement = new Vector2(Input.GetAxis("Horizontal"), 0) * speed; //turn this on for desktop controls
        movement = new Vector2(Input.acceleration.x, 0) * speed; //turn this on for android controls
        checkCoolDown();
    }

    void FixedUpdate()
    {
        rigidbody.velocity = movement;
    }

    public void SpawnEcho() {
        if (!coolingDown && playerResources.stamina > 0)
        {
            EventManager.TriggerEvent(EventTypes.ECHO_USED);
            currentCoolDownTime = echoCoolDownTime;
            Instantiate(Echo, new Vector3(PlayerPos.position.x, PlayerPos.position.y, -2), Quaternion.identity);
        }
    }

    //public void BloodSent() {
    //    if (!coolingDown) {
    //        EventManager.TriggerEvent(EventTypes.BLOOD_SENT);
    //        currentCoolDownTime = sentCoolDownTime;

            //Do Things
    //    }
    //}

    //public void ShapeShift() {
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    Destroy(GetComponent<PolygonCollider2D>());

    //    if (!isShapeShifted) {
    //        isShapeShifted = true;
    //        playerLight.spotAngle = draculaLightRange;
    //        spriteRenderer.sprite = draculaSprite;
    //        Debug.Log("ShapeShift");
    //    }
    //
    //    else if (isShapeShifted) {
    //        isShapeShifted = false;
    //        playerLight.spotAngle = batLightRange;
    //        spriteRenderer.sprite = batSprite;
    //        Debug.Log("Not ShapeShifted");
    //    }
    //
    //    gameObject.AddComponent<PolygonCollider2D>();
    //}

    void checkCoolDown()
    {
        if (currentCoolDownTime > 0)
        {
            coolingDown = true;
            currentCoolDownTime -= Time.deltaTime;
        }

        if (currentCoolDownTime <= 0)
        {
            currentCoolDownTime = 0;
            coolingDown = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            PlayerPrefs.SetFloat("playerScore", score.playerScore);
            LoadingController.LoadScene(LoadingController.Scenes.GAME_OVER);
            EventManager.TriggerEvent(EventTypes.GAME_OVER);
        }
    }
}
