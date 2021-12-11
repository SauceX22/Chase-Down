using UnityEngine;

public class OnDestroyEventSender : MonoBehaviour
{
    public event System.Action<OnDestroyEventSender> OnDestroyed;

    private void OnDestroy()
    {
        if (OnDestroyed != null)
            OnDestroyed(this);
    }
}