using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : IGameService
{
    private static PlayerManager current;

    private Dictionary<PlayerMovement.PlayerNumber, Color> playerColors = new Dictionary<PlayerMovement.PlayerNumber, Color>()
    {
        { PlayerMovement.PlayerNumber.One, Color.white },
        { PlayerMovement.PlayerNumber.Two, Color.white },
    };

    public bool twoPlayer = false;
    
    public static Action<PlayerMovement.PlayerNumber, Color> OnPlayerColorUpdated;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Initialize()
    {
        ServiceLocator.OnServiceLocaterLoaded += () => 
            ServiceLocator.Current.Register(new PlayerManager());
    }

    public void SetPlayerColor(PlayerMovement.PlayerNumber playerNumber, Color newColor)
    {
        foreach (KeyValuePair<PlayerMovement.PlayerNumber, Color> value in playerColors)
        {
            if (playerNumber == value.Key)
                continue;

            if (value.Value == newColor)
                return;
        }

        Debug.Log($"Setting {playerNumber} to {newColor}");
        playerColors[playerNumber] = newColor;
        OnPlayerColorUpdated?.Invoke(playerNumber, newColor);
    }

    public Color GetPlayerColor(PlayerMovement.PlayerNumber playerNumber)
    {
        return playerColors[playerNumber];
    }
}
