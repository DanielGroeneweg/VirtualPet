using UnityEngine;
using UnityEngine.Events;
public class StartUp : MonoBehaviour
{
    [SerializeField] PetValues petValues;
    public UnityEvent<float> Energy;
    private void Start()
    {
        Energy?.Invoke(petValues.LoadEngergy());
    }
}