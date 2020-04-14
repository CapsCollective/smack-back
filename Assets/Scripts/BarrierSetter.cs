using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSetter : MonoBehaviour
{
    private BarrierManager barrierManager;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        if (ServiceLocator.Current.Get<BarrierManager>() == null)
        {
            ServiceLocator.OnServiceRegistered += (s) =>
            {
                if (s is BarrierManager)
                {
                    barrierManager = ServiceLocator.Current.Get<BarrierManager>();
                    barrierManager.transforms.Add(new BarrierStuff()
                    {
                        offset = offset,
                        transform = transform
                    });
                }
            };
        }
        else
        {
            barrierManager = ServiceLocator.Current.Get<BarrierManager>();
            barrierManager.transforms.Add(new BarrierStuff()
            {
                offset = offset,
                transform = transform
            });
        }
    }
}
