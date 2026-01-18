using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "PetValues", menuName = "Scriptable Objects/PetValues")]
public class PetValues : ScriptableObject
{
    [SerializeField] private float energy = 0;
    [SerializeField] private float anger = 0;
    [SerializeField] private float coins = 0;
    public void ChangeEnergy(float change)
    {
        this.energy = change;
    }
    public void ChangeCoins(float change)
    {
        this.coins += change;
    }
    public void ChangeAnger(float change)
    {
        this.anger = change;
    }
    public float LoadEngergy()
    {
        return energy;
    }
    public float LoadCoins()
    {
        return energy;
    }
    public float LoadAnger()
    {
        return anger;
    }
}
