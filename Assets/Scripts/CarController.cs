using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public bool canMove = true;

    [Header("Stats")]
    public float maxFuel;
    public float fuel;
    public float fuelConsumptionRate;

    [Header("Ground Controlls")]
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;

    [Header("Air COntrolls")]
    public float groundRayLength = 1;
    public float rotationTorque = 2f;

    public bool isGrounded => 
        Physics.Raycast(wheels[0].transform.position, -wheels[0].transform.up, groundRayLength) 
        ||
        Physics.Raycast(wheels[1].transform.position, -wheels[1].transform.up, groundRayLength);

    private WheelControl[] wheels;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelControl>();
    }
    void FixedUpdate()
    {
        if (!canMove) return;

        float input = Input.GetAxis("Horizontal");

        // Calculate current speed along the car's forward axis
        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed)); // Normalized speed factor

        // Reduce motor torque and steering at high speeds for better handling
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        // Determine if the player is accelerating or trying to reverse
        bool isAccelerating = Mathf.Sign(input) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            if (isAccelerating)
            {
                wheel.WheelCollider.motorTorque = input * currentMotorTorque;
                wheel.WheelCollider.brakeTorque = 0f;

                if (input != 0)
                {
                    fuel -= fuelConsumptionRate * Time.fixedDeltaTime;

                    if (fuel <= 0)
                    {
                        fuel = 0;
                        canMove = false;
                    } 
                }
            }
            else
            {
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = Mathf.Abs(input) * brakeTorque;
            }
        }

        if (!isGrounded)
        {
            rigidBody.AddTorque(transform.right * input * rotationTorque, ForceMode.Force);
        }
    }

    public void Refuel()
    {
        fuel = maxFuel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // attached collider is of the character
        if (collision.gameObject.CompareTag("Ground"))
        {
            RacingGameManager.instance.EndRun();
        }
    }
}
