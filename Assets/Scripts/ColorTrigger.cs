using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorTrigger : MonoBehaviour
{
    [SerializeField] private MeshFilter[] colorSetters;
    [SerializeField] private Color color;

    private GameObject playerThatPickedMeUp;

    public static Action<GameObject, Color> OnColorPickup;

    void Start()
    {
        foreach (var item in colorSetters)
        {
            var colors = new Color[item.mesh.vertices.Length];
            for (int i = 0; i < colors.Length; i++)
                colors[i] = color;
            item.mesh.SetColors(colors);
        }

        OnColorPickup += (p, c) =>
        {
            if (playerThatPickedMeUp == p && c != color)
            {
                if (transform.GetChild(0) != null)
                    transform.GetChild(0).gameObject.SetActive(true);

                playerThatPickedMeUp = null;
            }
        };
    }

    void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Player"))
            return;

        if (!transform.GetChild(0).gameObject.activeSelf)
            return;

        var playerNumber = col.GetComponent<PlayerMovement>().playerNum;
        ServiceLocator.Current.Get<PlayerManager>().SetPlayerColor(playerNumber, color);

        if(transform.GetChild(0) != null)
            transform.GetChild(0).gameObject.SetActive(false);

        playerThatPickedMeUp = col.gameObject;

        OnColorPickup?.Invoke(col.gameObject, color);
    }


}
