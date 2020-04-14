using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Material playMat;
    public MeshRenderer screen;
    // Start is called before the first frame update
    void Start()
    {
        var pm = ServiceLocator.Current.Get<PlayerManager>();
        PlayerManager.OnPlayerColorUpdated += OpenDoor;
        OpenDoor(PlayerMovement.PlayerNumber.One, Color.black);
    }

    void OnDestroy()
    {
        PlayerManager.OnPlayerColorUpdated -= OpenDoor;
    }

    void OpenDoor(PlayerMovement.PlayerNumber p, Color col)
    {
        var pm = ServiceLocator.Current.Get<PlayerManager>();
        if (pm.GetPlayerColor(PlayerMovement.PlayerNumber.One) != Color.white ||
            pm.GetPlayerColor(PlayerMovement.PlayerNumber.Two) != Color.white)
        {
            screen.material = playMat;
            gameObject.SetActive(false);
        }
    }
}
