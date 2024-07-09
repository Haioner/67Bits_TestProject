using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private Renderer gfxRenderer;

    private void Start()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value);
        gfxRenderer.material.color = newColor;
    }
}
