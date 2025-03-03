using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class XPBar : MonoBehaviour
{
    public Slider xpSlider;
    public int xp = 0;
    public int xpMax = 100;
    public float fillSpeed = 0.5f; 

    void Start()
    {
        xpSlider.maxValue = xpMax;
        xpSlider.value = xp;
    }

    public void GainXP(int amount)
    {
        xp += amount;
        if (xp > xpMax) xp = xpMax;
        StartCoroutine(SmoothFill());
    }

    IEnumerator SmoothFill()
    {
        float targetValue = (float)xp / xpMax;
        while (xpSlider.value < targetValue)
        {
            xpSlider.value += fillSpeed * Time.deltaTime;
            yield return null;
        }
        xpSlider.value = targetValue;
    }
}
