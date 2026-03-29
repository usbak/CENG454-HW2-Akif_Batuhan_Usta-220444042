using UnityEngine;

public class flightController : MonoBehaviour
{  [SerializeField] private float pitchSpeed  = 45f;  // degrees/second 
    [SerializeField] private float yawSpeed    = 45f;  // degrees/second 
    [SerializeField] private float rollSpeed   = 45f;  // degrees/second 
    [SerializeField] private float thrustSpeed = 5f;   // units/second 
 
    private Rigidbody planeRb; 
 
    void Start() 
    { 
        void Start()
        {
            planeRb = GetComponent<Rigidbody>();
            planeRb.freezeRotation = true;
        }
    } 
 
    void Update()// or FixedUpdate() 
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

        float pitchAmount = verticalInput * pitchSpeed * Time.deltaTime;
        float yawAmount   = horizontalInput * yawSpeed * Time.deltaTime;
        float rollAmount  = rollInput * rollSpeed * Time.deltaTime;

        transform.Rotate(pitchAmount, yawAmount, rollAmount, Space.Self);  
 
    } 
 
    private void HandleThrust() 
    { 
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.forward * thrustSpeed * Time.deltaTime, Space.Self);
        }
    } 
    
}
