using System.Collections;
using UnityEngine;

public class SellTrigger : MonoBehaviour
{
    [SerializeField] private float delaySell = 0.2f;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private StackingManager _stackingManager;
    private bool _sellingCoins = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_sellingCoins)
        {
            StartCoroutine(SellCoinsRoutine());
        }
    }

    private IEnumerator SellCoinsRoutine()
    {
        _sellingCoins = true;
        playerMovement.CanMove = false;

        while (_stackingManager.HasStackCount())
        {
            _stackingManager.RemoveNPCFromStack();
            ShopManager.AddCoins(10);
            yield return new WaitForSeconds(delaySell);
        }

        playerMovement.CanMove = true;
        _sellingCoins = false;
    }
}
