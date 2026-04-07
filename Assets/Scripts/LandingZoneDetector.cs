using UnityEngine;

public class LandingZoneDetector : MonoBehaviour
{
    [SerializeField] FlightExamManager manager;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Landing trigger girdi: " + other.name + " tag: " + other.tag);

        if (!other.CompareTag("Player")) return;
        if (manager == null) { Debug.Log("manager null"); return; }

        Debug.Log("OnLanding cagriliyor");
        manager.OnLanding();
    }
}
