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

    public void FadeColor(Material mat, Color startColor, Color endColor, float fadeInDuration, float fadeOutDuration)
    {
        StartCoroutine(FadeColorCoroutine(mat, startColor, endColor, fadeInDuration, fadeOutDuration));
    }


    IEnumerator FadeColorCoroutine(Material mat,Color startColor, Color endColor, float fadeInDuration,float fadeOutDuration)
    {
        float t = 0;
        while (t < 1)
        {
            t = Mathf.Lerp(t, 1f, (1 / fadeInDuration) * Time.deltaTime);
            Color.Lerp(startColor, endColor, (1 / fadeInDuration) * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        t = 0;
        if(fadeOutDuration != 0)
        {
            while (t < 1)
            {
                t = Mathf.Lerp(t, 1f, (1 / fadeOutDuration) * Time.deltaTime);
                Color.Lerp(endColor, startColor, (1 / fadeOutDuration) * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}
