using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Controls the fading out of level's legend text.
/// </summary>
public class FadeOut : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float waitBeforeFade = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(FadeOutText));
    }

    // Fade the text off according to 
    private IEnumerator FadeOutText()
    {
        yield return new WaitForSeconds(waitBeforeFade);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, 
                text.color.g, 
                text.color.b, 
                text.color.a - (Time.deltaTime * speed)
            );
            yield return null;
        }
        // Remove once gone
        transform.gameObject.SetActive(false);
    }
}
