using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IGameService
{

}

public class ServiceLocator
{
	private ServiceLocator() { }

	private readonly Dictionary<string, IGameService> services = new Dictionary<string, IGameService>();

	public static ServiceLocator Current { get; private set; }

	public static Action OnServiceLocaterLoaded;
	public static Action<IGameService> OnServiceRegistered;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	public static void Initialize()
	{
		Debug.Log("Initializing Service Locator");
		Current = new ServiceLocator();
		OnServiceLocaterLoaded?.Invoke();
	}

	// Use this to get a service
	public T Get<T>() where T : IGameService
	{
		string key = typeof(T).Name;
		if (!services.ContainsKey(key))
		{
			Debug.LogError($"{key} not registered as service.");
			throw new InvalidOperationException();
		}
		return (T)services[key];
	}

	// Reigster a function with this
	public void Register<T>(T service) where T : IGameService
	{
		string key = typeof(T).Name;
		if (services.ContainsKey(key))
		{
			Debug.Log($"{key} already registered.");
			return;
		}

		services.Add(key, service);
		Debug.Log($"{key} added as service.");
	}

	public void Unregister<T>() where T : IGameService
	{
		string key = typeof(T).Name;
		if (!services.ContainsKey(key))
		{
			Debug.Log($"Attempted unregister of {key} which does not exist.");
			return;
		}

		services.Remove(key);
	}
}

