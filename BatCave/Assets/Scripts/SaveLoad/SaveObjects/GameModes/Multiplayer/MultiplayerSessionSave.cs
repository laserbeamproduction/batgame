using GooglePlayGames.BasicApi.Multiplayer;
using System;

/// <summary>
/// This class holds information about a single run in multiplayer mode.
/// </summary>
[Serializable]
public class MultiplayerSessionSave : SaveObject {

    private Participant player;
    private Participant opponent;
    private Participant winningPlayer;

    public MultiplayerSessionSave() {

    }

    public void Reset() {
        winningPlayer = null;
        player = null;
        opponent = null;
    }

    public void SetPlayerWon() {
        this.winningPlayer = this.player;
    }

    public void SetOpponentWon() {
        this.winningPlayer = this.opponent;
    }

    public Participant GetPlayer() {
        return this.player;
    }

    public Participant GetOpponent() {
        return this.opponent;
    }

    public void SetPlayers(Participant player, Participant opponent) {
        this.player = player;
        this.opponent = opponent;
    }

    public Participant GetWinningPlayer() {
        return this.winningPlayer;
    }

    public void SetWinningPlayer(Participant p) {
        this.winningPlayer = p;
    }
    
}
