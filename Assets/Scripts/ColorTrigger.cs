using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTrigger : MonoBehaviour
{
    [SerializeField] private Color color;

    void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Player"))
            return;


        var playerNumer = col.GetComponent<PlayerMovement>().playerNum;
        ServiceLocator.Current.Get<PlayerManager>().SetPlayerColor(playerNumer, color);
    }
}
