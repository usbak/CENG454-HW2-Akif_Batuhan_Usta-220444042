using UnityEngine;

public class TakeoffZoneDetector : MonoBehaviour
{
    [SerializeField] FlightExamManager manager;

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && manager != null)
            manager.OnTakeoff();
    }
}

