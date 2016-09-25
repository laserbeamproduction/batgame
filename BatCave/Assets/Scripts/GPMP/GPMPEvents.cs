public class GPMPEvents {

    public enum Types {

        // Match making
        GPMP_MATCH_MAKING_DONE,
        GPMP_SEARCH_QUICK_MATCH,
        GPMP_START_WITH_INVITE,
        GPMP_CANCEL_MATCH_MAKING,
        GPMP_LEAVE_GAME,
        GPMP_REPORT_ROOM_SETUP_PROGRESS,
        GPMP_VIEW_INVITES,

        // Game events
        GPMP_MATCH_INFO_READY,
        GPMP_UPDATE_MY_POSITION,
        GPMP_UPDATE_OPPONENT_POSITION,
        GPMP_MESSAGE_RECIEVED,
        GPMP_PLAYER_READY,
        GPMP_OPPONENT_READY,
        GPMP_START_GAME,
        GPMP_OPPONENT_LEFT
    };
}
