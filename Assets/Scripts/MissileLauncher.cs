using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] AudioSource fireSfx;

    GameObject currentMissile;

    public GameObject Launch(Transform target)
    {
        // ayni anda birden fazla fuze olmasin
        if (currentMissile != null)
            return currentMissile;

        if (missilePrefab == null || spawnPoint == null)
            return null;

        currentMissile = Instantiate(
            missilePrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        var brain = currentMissile.GetComponent<MissileHoming>();
        if (brain != null)
            brain.SetTarget(target);

        if (fireSfx != null)
            fireSfx.Play();

        return currentMissile;
    }

    public void DestroyActiveMissile()
    {
        if (currentMissile == null) return;

        Destroy(currentMissile);
        currentMissile = null;
    }
}