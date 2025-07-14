using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages a specific variable for a virtual pet, such as hunger or sleepiness.
/// This script should be placed on a child GameObject of the main Pet.
/// The GameObject's name will be used as the identifier for this variable and its Animator parameter.
/// </summary>
public class PetVariable : MonoBehaviour
{
    [Header("Variable Settings")]
    [Tooltip("The current value of this variable.")]
    public float value;
    
    [Tooltip("The minimum allowed value for this variable.")]
    public float minValue = 0f;
    
    [Tooltip("The maximum allowed value for this variable.")]
    public float maxValue = 100f;

    [Tooltip("The amount this variable changes per second (e.g., -1 for hunger, 0.5 for energy).")]
    public float changePerSecond = 0f;

    [Header("UI & Event Coupling")]
    [Tooltip("Event triggered when the PetVar's value changes, passing the new value.")]
    public UnityEvent<float> onValueChange;

    private Animator animator = null;
    private string varName = "";

    private float currentTime = 0f;

    bool VariableExistsInAnimator(Animator animator, string varName)
    {
        bool parameterWasFound = false;
        for (var i = 0; i < animator.parameterCount; i++)
        {
            var parameter = animator.GetParameter(i);
            if (parameter.name == varName)
            {
                parameterWasFound = true;
            }
        }
        return parameterWasFound;
    }

    void Awake()
    {
        varName = gameObject.name;

        animator = GetComponentInParent<Animator>();
        if (animator == null)
        {
            Debug.LogError($"PetVar '{varName}' could not find an Animator in its parent hierarchy. " +
                             "Please ensure the main Pet GameObject has an Animator component.", this);
            return;
        }

        if (!VariableExistsInAnimator(animator, varName))
        {
            Debug.LogWarning($"Animator parameter '{varName}' (derived from GameObject name) was not found in the Animator Controller of '{animator.name}'. " +
                                "Please ensure a Float parameter with this exact name exists in the Animator.", this);
            animator = null;//safety
        }

        SetValue(value);
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 1f)
        {
            int steps = Mathf.FloorToInt(currentTime);
            currentTime -= steps;

            SetValue(value + changePerSecond * steps);

            if (animator != null)
            {
                animator.SetFloat(varName, value);
            }
        }
    }

    /// <summary>
    /// Changes the PetVar's value by a relative amount.
    /// The value will be clamped between minValue and maxValue.
    /// </summary>
    /// <param name="amount">The amount to add to the current value (can be positive or negative).</param>
    public void ChangeValue(float amount)
    {
        SetValue(value + amount); // Re-use SetValue for clamping and event triggering
    }

    /// <summary>
    /// Sets the PetVar's value to an absolute new value.
    /// The value will be clamped between minValue and maxValue.
    /// </summary>
    /// <param name="newValue">The absolute new value to set.</param>
    public void SetValue(float newValue)
    {
        float clampedValue = Mathf.Clamp(newValue, minValue, maxValue);

        if (value != clampedValue)
        {
            value = clampedValue;
            onValueChange?.Invoke(value);
            value = Mathf.FloorToInt(clampedValue); 
        }
    }
}