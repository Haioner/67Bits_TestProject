using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(ItemShop))]
public class ColorSelector : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image colorImage;

    private Color _currentColor;
    private ItemShop _itemShop;
    private Button _currentButton;

    private void Start()
    {
        _itemShop = GetComponent<ItemShop>();
        _currentButton = GetComponent<Button>();
        _currentButton.onClick.AddListener(SelectColor);
    }

    public void InitiateItem(Color color)
    {
        _currentColor = color;
        colorImage.color = color;
    }

    public void SelectColor()
    {
        if (_itemShop.isBought)
        {
            if (_currentColor == Color.clear)
                PlayerColor.OnColorChange?.Invoke(Color.white);
            else
                PlayerColor.OnColorChange?.Invoke(_currentColor);

            SelectButtonImage();
        }
    }

    private void SelectButtonImage()
    {
        ShopManager.onSelectColorItem?.Invoke(transform);
    }
}
