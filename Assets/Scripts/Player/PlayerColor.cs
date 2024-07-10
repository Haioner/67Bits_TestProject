using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private Renderer gfxRenderer;

    public static System.Action<Color> OnColorChange;

    private void OnEnable()
    {
        OnColorChange += ChangeColor;
    }

    private void OnDisable()
    {
        OnColorChange -= ChangeColor;
    }

    private void ChangeColor(Color newColor)
    {
        gfxRenderer.material.color = newColor;
    }
}
