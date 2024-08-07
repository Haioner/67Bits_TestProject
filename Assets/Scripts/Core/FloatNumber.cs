using UnityEngine;
using TMPro;

public class FloatNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI floatNumberText;

    [Header("Destroy")]
    [SerializeField] private float delayToDestroy = 0.5f;
    [SerializeField] private float destroySpeed = 3f;
    [SerializeField] private CanvasGroup cg;

    private bool _canFadeOut;

    private void Update()
    {
        FadeOutCanvas();
    }

    public void InitFloatNumber(float numberValue, Color textColor)
    {
        floatNumberText.text = numberValue.ToString("F0");
        floatNumberText.color = textColor;
        Invoke(nameof(EnableFadeOut), delayToDestroy);
    }

    public void InitFloatNumber(string floatText, Color textColor)
    {
        floatNumberText.text = floatText;
        floatNumberText.color = textColor;
        Invoke(nameof(EnableFadeOut), delayToDestroy);
    }

    private void EnableFadeOut()
    {
        _canFadeOut = true;
    }

    private void FadeOutCanvas()
    {
        if (!_canFadeOut) return;

        cg.alpha = Mathf.MoveTowards(cg.alpha, 0, destroySpeed * Time.deltaTime);
        if (cg.alpha <= 0)
            Destroy(gameObject);
    }
}
