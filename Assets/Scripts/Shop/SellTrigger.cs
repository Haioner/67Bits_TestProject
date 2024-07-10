using System.Collections;
using UnityEngine;

public class SellTrigger : MonoBehaviour
{
    [SerializeField] private float delaySell = 0.2f;

    private StackingManager _stackingManager;
    private bool _sellingCoins = false;

    private void Start()
    {
        _stackingManager = FindFirstObjectByType<StackingManager>();
    }

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
        while (_stackingManager.HasStackCount())
        {
            _stackingManager.RemoveNPCFromStack();
            ShopManager.AddCoins(10);
            yield return new WaitForSeconds(delaySell);
        }
        _sellingCoins = false;
    }
}
