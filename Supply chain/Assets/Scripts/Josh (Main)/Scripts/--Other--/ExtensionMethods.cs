using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

static public class ExtensionMethods
{
    // (Color) Hex Colours
    static public Color HexColour(this Color col, string hex)
    {
        Color hexCol;
        ColorUtility.TryParseHtmlString(hex, out hexCol);
        return hexCol;
    }

    // (Image) Image Fading
    static public void ScreenFade(this Image spriteImage, float opacity, float duration, bool disableImage, Action evnt)
    {
        if (spriteImage.IsActive()) spriteImage.StartCoroutine(CoruScreenFade(spriteImage, opacity, duration, disableImage));

        IEnumerator CoruScreenFade(Image spriteImage, float opacity, float duration, bool disableImage)
        {
            float timeElapsed = 0;
            float startValue = spriteImage.color.a;
            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, opacity, timeElapsed / duration);
                spriteImage.color = new Color(spriteImage.color.r, spriteImage.color.g, spriteImage.color.b, newAlpha);
                yield return null;
            }
            spriteImage.StopAllCoroutines();
            if (disableImage) spriteImage.gameObject.SetActive(false);
            if (evnt != null) evnt.Invoke();
        }
    }

    // (Vector3) Single Value of Vector 3
    static public Vector3 Single(this Vector3 singleValue, float value)
    {return new Vector3(value, value, value);}
}