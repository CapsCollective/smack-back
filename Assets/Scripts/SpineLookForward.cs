using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineLookForward : MonoBehaviour
{
    [SerializeField] private Transform spine;
    [SerializeField] private bool freezeX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        spine.eulerAngles = new Vector3(0, 0, 0);
        if (freezeX)
        {
            spine.localPosition = new Vector3(0, spine.localPosition.y, 0);
            spine.rotation = Quaternion.identity;
        }
    }
}
