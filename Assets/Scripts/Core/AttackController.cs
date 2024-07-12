using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private string enemyTag = "NPC";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
            Attack(other);
    }

    public virtual void Attack(Collider other)
    {
        CinemachineShake.instance.ShakeCamera(1, 0.1f);

        Vector3 direction = (other.transform.position - transform.position).normalized;
        other.GetComponent<RagdollController>().TakePunch(direction);
    }
}
