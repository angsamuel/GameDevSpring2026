using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class FadeIn : MonoBehaviour
{

    Image coverImage;
    public float fadeTime = 1;

    public Color startColor;
    Color clearColor;
    void Awake()
    {
        coverImage = GetComponent<Image>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        clearColor = startColor;
        clearColor.a = 0;
        coverImage.color = startColor;
        StartCoroutine(FadeInRoutine());
    }

    IEnumerator FadeInRoutine()
    {
        float timer = 0;
        while(timer < fadeTime)
        {
            timer+=Time.deltaTime;
            coverImage.color = Color.Lerp(startColor,clearColor, timer/fadeTime);
            yield return null;
        }
        coverImage.color = clearColor;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
