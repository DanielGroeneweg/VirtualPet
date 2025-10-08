using DG.Tweening;
using UnityEngine;

public class CarCameraController : MonoBehaviour
{
    public Transform trackingTarget;
    public Vector3 offset;
    public float tweenDuration;

    private void Update()
    {
        transform.DOMove(trackingTarget.position + offset, tweenDuration);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
