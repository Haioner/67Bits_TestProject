using UnityEngine;

public class NPCAttack : AttackController
{
    public event System.EventHandler OnNPCAttack;

    public override void Attack(Collider other)
    {
        base.Attack(other);
        GetComponent<NPCMovement>().DisableMovement();
        other.GetComponent<PlayerMovement>().CanMove = false;
        other.GetComponent<StackingManager>().ResetStack();
        OnNPCAttack?.Invoke(this, System.EventArgs.Empty);
    }
}
