using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarrierManager : IGameService
{
    public List<Transform> transforms;

    public BarrierManager()
    {
        transforms = new List<Transform>();
        SceneManager.activeSceneChanged += (s, sc) => { transforms.Clear(); };
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Init()
    {
        ServiceLocator.OnServiceLocaterLoaded += () =>
            ServiceLocator.Current.Register(new BarrierManager());
    }
}
