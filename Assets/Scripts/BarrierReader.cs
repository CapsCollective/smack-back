using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierReader : MonoBehaviour
{
    private BarrierManager barrierManager;

    // Start is called before the first frame update
    void Start()
    {
        if (ServiceLocator.Current.Get<BarrierManager>() == null)
        {
            ServiceLocator.OnServiceRegistered += (s) =>
            {
                if (s is BarrierManager)
                {
                    barrierManager = ServiceLocator.Current.Get<BarrierManager>();
                }
            };
        }
        else
            barrierManager = ServiceLocator.Current.Get<BarrierManager>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector4> position = new List<Vector4>(4);
        foreach (var o in barrierManager.transforms)
        {
            position.Add(o.position);
        }

        Shader.SetGlobalVectorArray("PositionArray", position);
    }
}
