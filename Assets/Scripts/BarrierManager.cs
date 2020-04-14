using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarrierStuff
{
    public Transform transform;
    public Vector3 offset;
}

public class BarrierManager : IGameService
{
    public List<BarrierStuff> transforms;

    public BarrierManager()
    {
        transforms = new List<BarrierStuff>();
        SceneManager.activeSceneChanged += (s, sc) => { transforms.Clear(); };
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Init()
    {
        ServiceLocator.OnServiceLocaterLoaded += () =>
            ServiceLocator.Current.Register(new BarrierManager());
    }
}
