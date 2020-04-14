using UnityEngine;
using UnityEngine.EventSystems;

public class CallOnTrigger : MonoBehaviour
{
    public EventTrigger.TriggerEvent callback;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
            callback.Invoke(null);
    }
}