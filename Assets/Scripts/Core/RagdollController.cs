using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [Header("Ragdoll")]
    [SerializeField] private float knockbackForce = 40f;
    [SerializeField] private ParticleSystem hitParticle;

    [Header("CACHE")]
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody hipsRB;

    public void TakePunch(Vector3 knockbackDirection)
    {
        hipsRB.isKinematic = false;
        hipsRB.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

        anim.enabled = false;
        SpawnHitParticle();
    }

    private void SpawnHitParticle()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y += 1f;
        Instantiate(hitParticle, spawnPos, Quaternion.identity);
    }
}
