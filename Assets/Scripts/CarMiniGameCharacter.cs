using UnityEngine;

public class CarMiniGameCharacter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            RacingGameManager.instance.EndRun();
        }
    }
}
