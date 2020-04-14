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
        PlayerManager.OnPlayerColorUpdated += (p, c) =>
        {
            var pm = ServiceLocator.Current.Get<PlayerManager>();
            if (pm.GetPlayerColor(PlayerMovement.PlayerNumber.One) == Color.white ||
                pm.GetPlayerColor(PlayerMovement.PlayerNumber.Two) == Color.white) return;
            screen.material = playMat;
            gameObject.SetActive(false);
        };   
    }
}
