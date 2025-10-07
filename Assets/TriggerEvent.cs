using UnityEngine;
using UnityEngine.Events;
public class TriggerEvent : MonoBehaviour
{
    public UnityEvent animationEvent;
    public void TriggerAnimationEvent()
    {
        animationEvent?.Invoke();
    }
}