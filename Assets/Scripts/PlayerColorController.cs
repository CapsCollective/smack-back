using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerColorController : MonoBehaviour
{
    [SerializeField] private MeshFilter[] colorSetters;
    [SerializeField] private GameObject paddleGO;
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    PlayerMovement.PlayerNumber playerNumber;

    private void Start()
    {
        playerNumber = GetComponent<PlayerMovement>().playerNum;

        PlayerManager.OnPlayerColorUpdated += SetColor;

        if (ServiceLocator.Current.Get<PlayerManager>().GetPlayerColor(playerNumber) != Color.white)
        {
            paddleGO.SetActive(true);
            SetColor(playerNumber, ServiceLocator.Current.Get<PlayerManager>().GetPlayerColor(playerNumber));
        }
        else
        {
            paddleGO.SetActive(false);
            if (SceneManager.GetActiveScene().name == "PlayingField")
                paddleGO.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        PlayerManager.OnPlayerColorUpdated -= SetColor;
    }

    public void SetColor(PlayerMovement.PlayerNumber pN, Color newColor)
    {
        if (pN != playerNumber)
            return;

        foreach (var item in colorSetters)
        {
            if (item == null)
                return;

            var colors = new Color[item.mesh.vertices.Length];
            for (int i = 0; i < colors.Length; i++)
                colors[i] = newColor;
            item.mesh.SetColors(colors);
        }
        paddleGO.SetActive(true);
        audioSource1.Play();
        audioSource2.Play();
    }
}
