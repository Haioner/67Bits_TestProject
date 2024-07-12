using System.Collections;
using UnityEngine;

public class SellTrigger : MonoBehaviour
{
    [SerializeField] private float delaySell = 0.2f;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private StackingManager _stackingManager;
    [SerializeField] private FloatNumber floatNumberPrefab;
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
            InstantiateFloatNumber(10 + "<sprite=0>", Color.green);
            yield return new WaitForSeconds(delaySell);
        }

        playerMovement.CanMove = true;
        _sellingCoins = false;
    }


    private void InstantiateFloatNumber(string numberValue, Color textColor)
    {
        Vector3 randomOffset = Random.insideUnitCircle * 0.5f;
        Vector3 spawnPosition = _stackingManager.transform.position + randomOffset;
        spawnPosition.y += 2f;
        spawnPosition.z -= 2f;
        FloatNumber currentFloatNumber = Instantiate(floatNumberPrefab, spawnPosition, Quaternion.identity);
        currentFloatNumber.InitFloatNumber(numberValue, textColor);
    }
}
