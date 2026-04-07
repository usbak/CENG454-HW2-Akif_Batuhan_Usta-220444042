using UnityEngine;

public class flightController : MonoBehaviour
{
    [SerializeField] private float pitchSpeed  = 45f;
    [SerializeField] private float yawSpeed    = 45f;
    [SerializeField] private float rollSpeed   = 45f;
    [SerializeField] private float thrustSpeed = 15f;

    private Rigidbody planeRb;

    void Start()
    {
        planeRb = GetComponent<Rigidbody>();
        planeRb.freezeRotation = true;
        planeRb.useGravity = false; // ucak suzulsun, yere dusmesin
    }

    void FixedUpdate()
    {
        HandleRotation();
        HandleThrust();
    }

    private void HandleRotation()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float rollInput = 0f;

        if (Input.GetKey(KeyCode.Q)) rollInput = 1f;
        if (Input.GetKey(KeyCode.E)) rollInput = -1f;

        float pitchAmount = verticalInput * pitchSpeed * Time.fixedDeltaTime;
        float yawAmount   = horizontalInput * yawSpeed * Time.fixedDeltaTime;
        float rollAmount  = rollInput * rollSpeed * Time.fixedDeltaTime;

        Quaternion deltaRot = Quaternion.Euler(pitchAmount, yawAmount, rollAmount);
        planeRb.MoveRotation(planeRb.rotation * deltaRot);
    }

    private void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // velocity uzerinden hareket - collider'lara takilir
            planeRb.linearVelocity = transform.forward * thrustSpeed;
        }
        else
        {
            // space birakinca dur
            planeRb.linearVelocity = Vector3.zero;
        }
    }
}