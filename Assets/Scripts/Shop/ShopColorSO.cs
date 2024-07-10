using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemColor
{
    public Color ShopColor;
    public float ColorPrice;
}

[CreateAssetMenu (fileName ="Item Shop Color")]
public class ShopColorSO : ScriptableObject
{
    public List<ItemColor> ColorShopList = new List<ItemColor>(); 
}
