using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private string enemyTag = "NPC";
    [SerializeField] private AudioClip attackClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
            Attack(other);
    }

    public virtual void Attack(Collider other)
    {
        CinemachineShake.instance.ShakeCamera(1, 0.1f);
        SoundManager.PlayAudioClipPitch(attackClip, Random.Range(0.85f, 1.2f));
        Vector3 direction = (other.transform.position - transform.position).normalized;
        other.GetComponent<RagdollController>().TakePunch(direction);
    }
}
