using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] private ScalePunch scalePunch;
    [SerializeField] private Animator anim;
    [SerializeField] Transform hipsTransform;
    [SerializeField] private Rigidbody hipsRB;

    private NPCAttack _npcAttack;

    private void OnEnable()
    {
        if(TryGetComponent(out NPCAttack npcAttack))
        {
            _npcAttack = npcAttack;
            _npcAttack.OnNPCAttack += AttackAnim;
        }
    }

    private void OnDisable()
    {
        if(_npcAttack != null)
        {
            _npcAttack.OnNPCAttack -= AttackAnim;
        }
    }

    public void AttackAnim(object sender, System.EventArgs e)
    {
        anim.SetTrigger("Attack");
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

    public void RemoveParentAndKinematic()
    {
        hipsRB.isKinematic = false;
        transform.SetParent(null);
    }
}
