using UnityEngine;
using System.Collections;

public class EventTypes {

    public static string GAME_START = "gameStart";
    public static string GAME_PAUSED = "gamePaused";
    public static string GAME_RESUME = "gameResume";
    public static string GAME_OVER = "gameOver";

    public static string NEW_HIGHSCORE = "newHighscore";

    public static string HEALTH_PICKED_UP = "healthPickedUp";
    public static string ECHO_USED = "echoUsed";
    public static string ECHO_USED_RESOURCES = "echoUsedResources";
    public static string SKILL_VALUE = "skillValue";
    public static string SET_DAY_TIME = "setDayTime";
    public static string SET_NIGHT_TIME = "setNightTime";
    public static string SHAPE_SHIFT = "shapeShift";
    public static string BLOOD_SENT = "bloodSent";
    public static string PLAYER_SPEED_CHANGED = "playerSpeedChanged";
    public static string PLAYER_DIED = "playerDied";
    public static string PLAYER_FLY_IN = "playerFlyIn";
    public static string PLAYER_TAKES_DAMAGE = "playerTakesDamage";
    public static string ENABLE_PLAYER_LIGHT = "enablePlayerLight";
    public static string DISABLE_PLAYER_LIGHT = "disablePlayerLight";
    public static string PLAYER_IN_POSITION = "playerInPosition";

    public static string PLAYER_SHIELD_PICKUP = "playerShieldPickUp";
    public static string PLAYER_SHIELD_ENDED = "playerShieldEnded";

    public static string PLAYER_SPEED_PICKUP = "playerSpeedPickUp";
    public static string PLAYER_SPEED_ENDED = "playerSpeedEnded";

    //Tension Moments
    public static string START_TENSION = "startTension";
    public static string STOP_TENSION = "stopTension";

    public static string START_SPAWNING = "startSpawning";
    public static string STOP_SPAWNING = "stopSpawning";

    public static string ACTIVATE_POWERUPS = "activatePowerups";

    // Multiplayer 
}
