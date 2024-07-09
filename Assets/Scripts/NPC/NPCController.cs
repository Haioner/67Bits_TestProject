using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Ragdoll")]
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private ParticleSystem hitParticle;

    [Header("CACHE")]
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Animator _anim;
    [SerializeField] Transform hipsTransform;
    [SerializeField] private Rigidbody rb;

    public void TakePunch(Vector3 knockbackDirection)
    {
        rb.isKinematic = false;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

        _anim.enabled = false;
        capsuleCollider.enabled = false;
        SpawnHitParticle();
    }

    public void ResetToStack()
    {
        rb.isKinematic = true;
        hipsTransform.localPosition = Vector3.zero;
        Quaternion newRotation = Quaternion.identity;
        hipsTransform.localRotation = newRotation;
    }

    private void SpawnHitParticle()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y += 1f;
        Instantiate(hitParticle, spawnPos, Quaternion.identity);
    }
}
