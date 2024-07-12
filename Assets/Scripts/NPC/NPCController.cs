using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] private ScalePunch scalePunch;
    [SerializeField] private Animator anim;
    [SerializeField] Transform hipsTransform;
    [SerializeField] private Rigidbody hipsRB;
    [SerializeField] private RagdollController ragdollController;
    [SerializeField] private NPCMovement npcMovement;

    private NPCAttack _npcAttack;
    private Vector3 _initialPos;
    private CapsuleCollider _capsuleCollider;

    private void OnEnable()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _initialPos = transform.position;
        if(TryGetComponent(out NPCAttack npcAttack))
        {
            _npcAttack = npcAttack;
            _npcAttack.OnNPCAttack += AttackAnim;
        }

        scalePunch.OnDeactive += ResetNPC;
    }

    private void OnDisable()
    {
        if(_npcAttack != null)
        {
            _npcAttack.OnNPCAttack -= AttackAnim;
        }
        scalePunch.OnDeactive -= ResetNPC;
    }

    private void ResetNPC(object sender, System.EventArgs e)
    {
        _capsuleCollider.enabled = true;
        transform.SetParent(null);
        ragdollController.DisableRagdoll();
        transform.position = _initialPos;
        transform.rotation = Quaternion.identity;

        npcMovement.enabled = true;
        npcMovement.SetInitialRotation();
    }

    public void AttackAnim(object sender, System.EventArgs e)
    {
        anim.SetTrigger("Attack");
    }

    public void SellNPC()
    {
        scalePunch.DoPunch(true);
    }

    public void MoveToStackPosRot()
    {
        npcMovement.enabled = false;
        hipsRB.isKinematic = true;
        ragdollController.ResetHips();
        scalePunch.DoPunch();
    }

    public void RemoveParentAndKinematic()
    {
        hipsRB.isKinematic = false;
        transform.SetParent(null);
    }
}
