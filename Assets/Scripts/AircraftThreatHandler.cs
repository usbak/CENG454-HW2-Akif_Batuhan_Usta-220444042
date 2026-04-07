using UnityEngine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AircraftThreatHandler : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] AudioSource impactSfx;
    [SerializeField] FlightExamManager manager;

    Rigidbody body;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Missile")) return;

        // patlama sesi
        if (impactSfx != null) impactSfx.Play();

        // gelen fuzeyi yok et
        Destroy(other.gameObject);

        // manager'a haber ver
        if (manager != null) manager.OnMissileHit();

        // ucagi basa al
        ResetToStart();
    }

    void ResetToStart()
    {
        if (startPoint == null) return;

        body.linearVelocity  = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);
    }
}
