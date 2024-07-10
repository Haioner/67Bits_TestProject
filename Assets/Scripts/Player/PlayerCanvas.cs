using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stackText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private CanvasGroup shopCanvasGroup;

    [Header("Player Level")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private float progressSpeed = 4.2f;
    private float _currentXP;
    private float _currentmaxXP;

    private void OnEnable()
    {
        ShopManager.onCoinChange += UpdateCoinsText;
        StackingManager.onStackChange += UpdateStackText;

        //Level
        PlayerLevel.onLevelChanged += UpdateLevelText;
        PlayerLevel.onXPChanged += UpdateXPValue;
    }

    private void OnDisable()
    {
        ShopManager.onCoinChange -= UpdateCoinsText;
        StackingManager.onStackChange -= UpdateStackText;

        //Level
        PlayerLevel.onLevelChanged -= UpdateLevelText;
        PlayerLevel.onXPChanged -= UpdateXPValue;
    }

    private void Update()
    {
        UpdateXPSlider();
    }

    #region Level
    private void UpdateLevelText(int levelValue)
    {
        levelText.text = "Level:" + levelValue.ToString();
    }

    private void UpdateXPValue(float currentXP, float maxXPValue)
    {
        _currentXP = currentXP;
        _currentmaxXP = maxXPValue;
    }

    private void UpdateXPSlider()
    {
        if (_currentXP < 0) return;
        xpSlider.maxValue = _currentmaxXP;
        xpSlider.value = Mathf.MoveTowards(xpSlider.value, _currentXP, SpeedProgress());
    }

    private float SpeedProgress()
    {
        return progressSpeed * (_currentmaxXP / 5) * Time.deltaTime;
    }
    #endregion

    private void UpdateCoinsText(float coinsValue)
    {
        coinText.text = coinsValue.ToString("F0");
    }

    private void UpdateStackText(int stack, int maxStack)
    {
        stackText.text = stack.ToString() + "/" + maxStack.ToString();
    }

    public void SwitchShopCanvas()
    {
        if(shopCanvasGroup.alpha == 1) //On to Off
        {
            shopCanvasGroup.alpha = 0;
            shopCanvasGroup.interactable = false;
            shopCanvasGroup.blocksRaycasts = false;
        }
        else //Off to On
        {
            shopCanvasGroup.alpha = 1;
            shopCanvasGroup.interactable = true;
            shopCanvasGroup.blocksRaycasts = true;
        }
    }
}
