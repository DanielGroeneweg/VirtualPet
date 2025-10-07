using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public float speed;
    public float breakSpeed;
    public float rotationSpeed;

    public float groundCheckDistance;
    public bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (isGrounded)
                rigidbody.AddForce(Vector3.right * speed);
            else
                rigidbody.AddTorque(Vector3.forward * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (isGrounded)
                rigidbody.AddForce(Vector3.left * breakSpeed); 
            else
                rigidbody.AddTorque(Vector3.back * rotationSpeed);
        }
    }
}
