using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Colors
{
    public Color red;
    public Color orange;
    public Color yellow;

    public Color[] possibleColors;
}

public class Helper : MonoBehaviour
{
    public Colors colors;
    public float TileFadeInDuration;
    public float TileFadeOutDuration;

    public Material blackMaterial;
    public Material whiteMaterial;

    // Start is called before the first frame update
    void Start()
    {
        colors = new Colors();
        colors.red = new Color(1, 1, 1);
        /*colors.orange = new Color(250, 110, 32);
        colors.yellow = new Color32(255,249,73,255);*/

        colors.possibleColors = new Color[1] {colors.red/*, colors.orange, colors.yellow*/};
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public void FadeColor(MeshRenderer meshRenderer, Color startColor1, Color endColor1, Color startColor2, Color endColor2, float fadeInDuration, float fadeOutDuration, bool tile)
    {
        StartCoroutine(FadeColorCoroutine(meshRenderer, startColor1, endColor1, startColor2, endColor2, fadeInDuration, fadeOutDuration, tile));
    }


    IEnumerator FadeColorCoroutine(MeshRenderer meshRenderer, Color startColor1, Color endColor1, Color startColor2, Color endColor2, float fadeInDuration, float fadeOutDuration, bool tile)
    {
        if (tile)
        {
            meshRenderer.material = whiteMaterial;
        }

        for (float a =0; a <= fadeInDuration; a+= Time.deltaTime)
        {
            meshRenderer.material.color = Color.Lerp(startColor1, endColor1, a/fadeInDuration);
            yield return null;
        }

        for (float b = 0; b <= fadeOutDuration; b += Time.deltaTime)
        {
            meshRenderer.material.color = Color.Lerp(startColor2, endColor2, b/fadeOutDuration);
            yield return null;
        }

        if (tile)
        {
            meshRenderer.material = blackMaterial;
        }

    }
}
