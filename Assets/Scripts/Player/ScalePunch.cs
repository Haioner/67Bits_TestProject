using System.Collections;
using UnityEngine;

public class ScalePunch : MonoBehaviour
{
    [SerializeField] private Vector3 punchScale = new Vector3(1.5f, 1.2f, 1f);
    [SerializeField] private float punchDuration = 0.1f;
    [SerializeField] private float fadeDuration = 0.1f;

    private Vector3 _originalScale;
    private Coroutine _punchCoroutine;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    public void DoPunch()
    {
        if (_punchCoroutine != null)
            StopCoroutine(_punchCoroutine);
        
        _punchCoroutine = StartCoroutine(PunchCoroutine());
    }

    private IEnumerator PunchCoroutine()
    {
        transform.localScale = punchScale;
        yield return new WaitForSeconds(punchDuration);

        //fade-out
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            transform.localScale = Vector3.Lerp(punchScale, _originalScale, t);

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = _originalScale;
    }
}
