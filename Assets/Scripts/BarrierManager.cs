using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierManager : IGameService
{
    public List<Transform> transforms;

    public BarrierManager()
    {
        transforms = new List<Transform>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Init()
    {
        ServiceLocator.OnServiceLocaterLoaded += () =>
            ServiceLocator.Current.Register(new BarrierManager());
    }
}
