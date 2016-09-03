using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;


/// <summary>
/// This helper class functions as a layer between the Google Play 
/// plugin code and the game code.
/// </summary>
public class GooglePlayHelper {

    public static GooglePlayHelper instance;

    private bool isInitialized;
    private PlayGamesClientConfiguration config;

    public static GooglePlayHelper GetInstance() {
        if (instance == null)
            instance = new GooglePlayHelper();
        return instance;
    }

    public GooglePlayHelper() {
        if (!isInitialized)
            Init();
    }

    /// <summary>
    /// Initializes Google Play Service
    /// </summary>
    public void Init() {
        // Set Google Play Service as Social platform
        GooglePlayGames.PlayGamesPlatform.Activate();

        // Enables saving game progress.
        config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        isInitialized = true;
    }

    /// <summary>
    /// Starts Google Authentication flow for authenticating player
    /// </summary>
    public void Login() {
        Social.localUser.Authenticate((bool success) => {
            if (success) {
                Debug.Log("Login succes");
            } else {
                Debug.Log("Login failed!");
            }
        });
    }

    /// <summary>
    /// Logs out current active user from Google Play
    /// </summary>
    public void Logout() {
        ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
    }

    /// <summary>
    /// Send savedata to Google Play service as an array of bytes
    /// </summary>
    /// <param name="game"></param>
    /// <param name="savedData"></param>
    /// <param name="totalPlaytime"></param>
    public void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime) {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
       

        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    /// <summary>
    /// Callback on function SaveGame() holding the result
    /// </summary>
    /// <param name="status"></param>
    /// <param name="game"></param>
    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game) {
        if (status == SavedGameRequestStatus.Success) {
            // handle reading or writing of saved game.
        } else {
            // handle error
        }
    }

    /// <summary>
    /// Load savegame data from Google Play Service
    /// </summary>
    /// <param name="game"></param>
    public void LoadGameData(ISavedGameMetadata game) {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    /// <summary>
    /// Callback from function LoadGameData() holding the result
    /// </summary>
    /// <param name="status"></param>
    /// <param name="data"></param>
    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data) {
        if (status == SavedGameRequestStatus.Success) {
            // handle processing the byte array data
            if (data.Length == 0) {
                // no save file is present
            }
        } else {
            // handle error
        }
    }
}
