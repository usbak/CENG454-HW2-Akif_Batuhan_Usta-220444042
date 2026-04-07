using UnityEngine;

public class MissileHoming : MonoBehaviour
{
    [SerializeField] float flySpeed = 20f;
    [SerializeField] float rotateSpeed = 3f;

    Transform chaseTarget;

    public void SetTarget(Transform t)
    {
        chaseTarget = t;
    }

    void Update()
    {
        // hedef yoksa kendini sil
        if (chaseTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        Steer();
        MoveForward();
    }

    void Steer()
    {
        Vector3 toTarget = chaseTarget.position - transform.position;
        if (toTarget.sqrMagnitude < 0.0001f) return;

        Quaternion desired = Quaternion.LookRotation(toTarget.normalized);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desired,
            rotateSpeed * Time.deltaTime
        );
    }

    void MoveForward()
    {
        transform.position += transform.forward * flySpeed * Time.deltaTime;
    }
}