using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Scrollbar hpScrollbar;
    public Scrollbar xpScrollbar;
    public Scrollbar goldScrollbar;

    private float hpValue = 1f;
    private float xpValue = 0.5f;
    private float goldValue = 0.2f;

    void Start()
    {
        if (hpScrollbar != null)
            hpScrollbar.size = hpValue;

        if (xpScrollbar != null)
            xpScrollbar.size = xpValue;

        if (goldScrollbar != null)
            goldScrollbar.size = goldValue;
    }
    public void UpdateHP(float value)
    {
        if (hpScrollbar != null)
            hpScrollbar.size = value;
    }

    public void UpdateXP(float value)
    {
        if (xpScrollbar != null)
            xpScrollbar.size = Mathf.Clamp01(value);
    }

    public void UpdateGold(float value)
    {
        if (goldScrollbar != null)
            goldScrollbar.size = Mathf.Clamp01(value);
    }
}
