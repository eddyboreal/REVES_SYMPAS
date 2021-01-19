using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Colors
{
    public Color red;
    public Color blue;
    public Color yellow;
    public Color green;

    public Color[] possibleColors;
}

public class Helper : MonoBehaviour
{
    public Colors colors;
    // Start is called before the first frame update
    void Start()
    {
        colors = new Colors();
        colors.red = new Color(1, 0, 0);
        colors.blue = new Color(0, 0, 1);
        colors.yellow = new Color(1, 1, 0);
        colors.green = new Color(0, 1, 0);

        colors.possibleColors = new Color[4] { colors.red, colors.blue, colors.yellow, colors.green };
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public void FadeColor(MeshRenderer meshRenderer, Color startColor, Color endColor, float fadeInDuration, float fadeOutDuration)
    {
        StartCoroutine(FadeColorCoroutine(meshRenderer, startColor, endColor, fadeInDuration, fadeOutDuration));
    }


    IEnumerator FadeColorCoroutine(MeshRenderer meshRenderer, Color startColor, Color endColor, float fadeInDuration,float fadeOutDuration)
    {

        for(float a =0; a <= fadeInDuration; a+= Time.deltaTime)
        {
            meshRenderer.material.color = Color.Lerp(startColor, endColor, a/fadeInDuration);
            yield return null;
        }

        for (float b = 0; b <= fadeOutDuration; b += Time.deltaTime)
        {
            meshRenderer.material.color = Color.Lerp(endColor, startColor, b/fadeOutDuration);
            yield return null;
        }
    }
}
