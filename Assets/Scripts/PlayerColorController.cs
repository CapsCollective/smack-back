using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorController : MonoBehaviour
{
    [SerializeField] private MeshFilter[] colorSetters;
    [SerializeField] private GameObject paddleGO;

    PlayerMovement.PlayerNumber playerNumber;

    private void Start()
    {
        playerNumber = GetComponent<PlayerMovement>().playerNum;

        PlayerManager.OnPlayerColorUpdated += (n, c) =>
        {
            if (playerNumber == n)
                SetColor(c);
        };

        SetColor(ServiceLocator.Current.Get<PlayerManager>().GetPlayerColor(playerNumber));
        if (ServiceLocator.Current.Get<PlayerManager>().GetPlayerColor(playerNumber) != Color.white)
            paddleGO.SetActive(true);
        else
            paddleGO.SetActive(false);
    }

    public void SetColor(Color newColor)
    {
        foreach (var item in colorSetters)
        {
            var colors = new Color[item.mesh.vertices.Length];
            for (int i = 0; i < colors.Length; i++)
                colors[i] = newColor;
            item.mesh.SetColors(colors);
        }
        paddleGO.SetActive(true);
    }
}
