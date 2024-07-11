using UnityEngine;

public class PlayerAttack : AttackController
{
    [SerializeField] private StackingManager stackingManager;
    public static event System.EventHandler OnPlayerAttack;

    public override void Attack(Collider other)
    {
        NPCController npcController = other.GetComponent<NPCController>();
        if (npcController != null && stackingManager.AvailableStack())
        {
            base.Attack(other);
            stackingManager.AddNPCToStack(npcController);
            OnPlayerAttack?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
