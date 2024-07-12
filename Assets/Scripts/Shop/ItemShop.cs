using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Button))]
public class ItemShop : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private ParticleSystem buyParticle;

    [Header("Item")]
    [SerializeField] private float initialPrice;
    [SerializeField] private float priceMultiplier = 1.2f;
    [SerializeField] private int maxBuyCount = 1;

    [Space]
    [SerializeField] private UnityEvent buyEvent;
    public bool isBought { get; private set; }

    public float CurrentPrice { get; private set; }
    private int _currentBuyCount = 1;
    private Button _itemButton;

    public void InitiateItem(float newInitialPrice)
    {
        initialPrice = newInitialPrice;
        CurrentPrice = initialPrice;
        UpdatePriceText(ShopManager.Coins);
    }

    private void OnEnable()
    {
        _itemButton = GetComponent<Button>();
        _itemButton.onClick.AddListener(Buy);

        CurrentPrice = initialPrice;
        CheckBought();

        ShopManager.onCoinChange += UpdatePriceText;
    }

    private void OnDisable()
    {
        ShopManager.onCoinChange -= UpdatePriceText;
    }

    public void Buy()
    {
        if (CurrentPrice > ShopManager.Coins) return;

        if (_currentBuyCount < maxBuyCount + 1)
        {
            ShopManager.RemoveCoins(CurrentPrice);
            UpdatePrice();
            _currentBuyCount++;
            buyEvent?.Invoke();
            CheckBought();
        } 
    }

    private void CheckBought()
    {
        if (_currentBuyCount >= maxBuyCount + 1)
        {
            priceText.gameObject.SetActive(false);
            isBought = true;
        }
    }

    private void UpdatePrice()
    {
        CurrentPrice *= priceMultiplier;
        UpdatePriceText(ShopManager.Coins);
    }

    private void UpdatePriceText(float coin)
    {
        if (coin >= CurrentPrice)
            priceText.color = Color.green;
        else
            priceText.color = Color.red;

        priceText.text = CurrentPrice.ToString("F0") + "<sprite=0>";
    }

    public void SpawnBuyParticle(Transform buttonTransform)
    {
        Instantiate(buyParticle, buttonTransform);
    }
}
