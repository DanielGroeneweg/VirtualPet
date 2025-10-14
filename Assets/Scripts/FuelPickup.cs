using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            RacingGameManager.instance.car.Refuel();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // destory self if player has passed, allowing spawning a new pickup
        if(RacingGameManager.instance.distanceTraveled - 10 > transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
