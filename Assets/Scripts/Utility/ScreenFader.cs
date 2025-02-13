using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour
{
    public float startAlpha = 1f;
    public float targetAlpha = 0f;
    public float delay = 0f;
    public float timeToFade = 1f;

    float inc;
    float currentAlpha;
    MaskableGraphic graphic;
    Color originalColor;

    private void Start()
    {
        graphic = GetComponent<MaskableGraphic>();
        originalColor = graphic.color;
        currentAlpha = startAlpha;

        Color tempColor = new Color(originalColor.r, originalColor.g, originalColor.b, currentAlpha);

        graphic.color = tempColor;
        inc = ((targetAlpha - startAlpha) / timeToFade) * Time.deltaTime;

        StartCoroutine("FadeRoutine");
    }

    IEnumerator FadeRoutine()
    {

    }
}
