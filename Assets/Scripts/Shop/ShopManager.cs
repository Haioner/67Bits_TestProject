using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Color Shop")]
    [SerializeField] private ItemShop colorItemShop;
    [SerializeField] private Transform colorItemContent;
    [SerializeField] private ShopColorSO colorList;
    [SerializeField] private RectTransform itemSelection;
    [SerializeField] private AudioClip selectClip;
    public static System.Action<Transform> onSelectColorItem;

    public static float Coins { get; private set; }

    //Invoke coin change
    public delegate void OnCoinChange(float coinValue);
    public static event OnCoinChange onCoinChange;

    private void OnEnable()
    {
        Coins = 0;
        onSelectColorItem += SelectItem;
    }

    private void OnDisable()
    {
        onSelectColorItem -= SelectItem;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.X))
            AddCoins(99999);
#endif
    }

    private void Start()
    {
        onCoinChange?.Invoke(Coins);
        SpawnColorsShop();
    }

    public static void AddCoins(float valueToAdd)
    {
        Coins += valueToAdd;
        onCoinChange?.Invoke(Coins);
    }

    public static void RemoveCoins(float valueToRemove)
    {
        Coins -= valueToRemove;
        onCoinChange?.Invoke(Coins);
    }

    public void SpawnColorsShop()
    {
        foreach (var item in colorList.ColorShopList)
        {
            ItemShop currentItem = Instantiate(colorItemShop, colorItemContent);
            currentItem.InitiateItem(item.ColorPrice);
            currentItem.GetComponent<ColorSelector>().InitiateItem(item.ShopColor);
        }
    }

    private void SelectItem(Transform itemParent)
    {
        itemSelection.SetParent(itemParent);
        itemSelection.anchoredPosition = Vector2.zero;
        SoundManager.PlayAudioClip(selectClip);
    }
}
