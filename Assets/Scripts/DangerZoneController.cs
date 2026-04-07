// DangerZoneController.cs
using UnityEngine;

public class DangerZoneController : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private MissileLauncher missileLauncher;
    [SerializeField] private float missileDelay = 5f;

    private Coroutine activeCountdown;
    private bool playerInZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;

            // HUD uyarısını hemen göster
            examManager.EnterDangerZone();

            // 5 saniye sonra füze fırlat
            activeCountdown = StartCoroutine(MissileCountdown());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;

            // Geri sayımı iptal et
            if (activeCountdown != null)
            {
                StopCoroutine(activeCountdown);
                activeCountdown = null;
            }

            // Aktif füzeyi yok et
            if (missileLauncher != null)
            {
                missileLauncher.DestroyActiveMissile();
            }

            // HUD'u güncelle
            examManager.ExitDangerZone();
        }
    }

    private System.Collections.IEnumerator MissileCountdown()
    {
        yield return new WaitForSeconds(missileDelay);

        // 5 saniye sonra hâlâ bölgedeyse füze fırlat
        if (playerInZone && missileLauncher != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                missileLauncher.Launch(player.transform);
            }
        }

        activeCountdown = null;
    }
}