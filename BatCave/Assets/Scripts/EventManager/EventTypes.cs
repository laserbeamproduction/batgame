using UnityEngine;
using System.Collections;

public class EventTypes {

    public const string GAME_START = "gameStart";
    public const string GAME_PAUSED = "gamePaused";
    public const string GAME_RESUME = "gameResume";
    public const string GAME_OVER = "gameOver";

    public const string NEW_HIGHSCORE = "newHighscore";

    public const string HEALTH_PICKED_UP = "healthPickedUp";
    public const string ECHO_USED = "echoUsed";
    public const string ECHO_USED_RESOURCES = "echoUsedResources";
    public const string SKILL_VALUE = "skillValue";
    public const string SHAPE_SHIFT = "shapeShift";
    public const string BLOOD_SENT = "bloodSent";
    public const string PLAYER_SPEED_CHANGED = "playerSpeedChanged";
    public const string PLAYER_DIED = "playerDied";
    public const string PLAYER_FLY_IN = "playerFlyIn";
    public const string PLAYER_TAKES_DAMAGE = "playerTakesDamage";
    public const string ENABLE_PLAYER_LIGHT = "enablePlayerLight";
    public const string DISABLE_PLAYER_LIGHT = "disablePlayerLight";
    public const string PLAYER_IN_POSITION = "playerInPosition";

    //NETWORK 
    public const string SERVER_STARTED = "serverStarted";
    public const string PLAYER_TWO_JOINED = "playerTwoJoined";
    public const string START_MATCH = "startMatch";
    public const string START_COUNTDOWN = "startCountdown";
    public const string INSTANTIATE_OBJECT_POOL = "instantiateObjectPool";
    public const string PLAY_ONLINE_PRESSED = "playOnlinePressed";
    public const string HIDE_LOBBY = "hideLobby";
    public const string RESTART_SEARCH = "restartSearch";
}
