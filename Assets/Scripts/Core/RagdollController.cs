using UnityEngine.Events;
using System.Collections;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [Header("Ragdoll")]
    [SerializeField] private float knockbackForce = 40f;
    [SerializeField] private ParticleSystem hitParticle;

    [Header("CACHE")]
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody hipsRB;
    [SerializeField] private GameObject characterObject;
    [SerializeField] private GameObject ragdollObject;

    [Space]
    [SerializeField] private UnityEvent OnHitEvent;

    private Vector3 _initialHipsPos;
    private Quaternion _initialHipsRot;

    private void Start()
    {
        if (hipsRB != null)
        {
            _initialHipsPos = hipsRB.transform.localPosition;
            _initialHipsRot = hipsRB.transform.localRotation;
        }
    }

    public void TakePunch(Vector3 knockbackDirection)
    {
        if (ragdollObject != null)
        {
            characterObject.SetActive(false);
            ragdollObject.SetActive(true);
        }

        if (TryGetComponent(out CapsuleCollider capsuleCollider))
            capsuleCollider.enabled = false;

        hipsRB.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        SpawnHitParticle();

        OnHitEvent?.Invoke();
    }

    public void DisableRagdoll()
    {
        StartCoroutine(DelayDisable());
    }

    private IEnumerator DelayDisable()
    {
        anim.enabled = true;
        yield return new WaitForSeconds(0.1f);
        characterObject.SetActive(true);
        ragdollObject.SetActive(false);
        hipsRB.isKinematic = false;
        anim.enabled = false;
        ResetHips();
    }

    public void ResetHips()
    {
        if (!hipsRB.isKinematic)
            hipsRB.velocity = Vector3.zero;

        hipsRB.transform.localPosition = _initialHipsPos;
        hipsRB.transform.localRotation = _initialHipsRot;
    }

    private void SpawnHitParticle()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y += 1f;
        Instantiate(hitParticle, spawnPos, Quaternion.identity);
    }
}
