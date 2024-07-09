using UnityEngine;
using System;

public class AttackController : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private StackingManager stackingManager;
    public static event EventHandler OnAttack;

    private void OnTriggerEnter(Collider other)
    {
        Attack(other);
    }

    private void Attack(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            NPCController npcController = other.GetComponent<NPCController>();
            if (npcController != null)
            {
                stackingManager.AddNPCToStack(npcController);

                Vector3 direction = (other.transform.position - transform.position).normalized;
                npcController.TakePunch(direction);

                //Effects-GFX
                CinemachineShake.instance.ShakeCamera(5,0.1f);
                OnAttack?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
