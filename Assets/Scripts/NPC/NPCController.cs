using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Ragdoll")]
    [SerializeField] private float knockbackForce = 40f;
    [SerializeField] private ParticleSystem hitParticle;

    [Header("CACHE")]
    [SerializeField] private ScalePunch scalePunch;
    [SerializeField] private Animator anim;
    [SerializeField] Transform hipsTransform;
    [SerializeField] private Rigidbody hipsRB;

    private CapsuleCollider _capsuleCollider;

    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void TakePunch(Vector3 knockbackDirection)
    {
        hipsRB.isKinematic = false;
        hipsRB.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

        anim.enabled = false;
        _capsuleCollider.enabled = false;
        SpawnHitParticle();
    }

    public void ScalePunchDestroy()
    {
        scalePunch.DoPunch(true);
    }

    public void ResetPosRot()
    {
        hipsRB.isKinematic = true;
        hipsTransform.localPosition = Vector3.zero;
        Quaternion newRotation = Quaternion.identity;
        hipsTransform.localRotation = newRotation;
        scalePunch.DoPunch();
    }

    private void SpawnHitParticle()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y += 1f;
        Instantiate(hitParticle, spawnPos, Quaternion.identity);
    }
}
