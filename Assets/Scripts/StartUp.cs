using UnityEngine;
using UnityEngine.Events;
public class StartUp : MonoBehaviour
{
    [SerializeField] PetValues petValues;
    public UnityEvent<float> Energy;
    public UnityEvent<float> Anger;
    private void Start()
    {
        Energy?.Invoke(petValues.LoadEngergy());
        Anger?.Invoke(petValues.LoadAnger());
    }
}