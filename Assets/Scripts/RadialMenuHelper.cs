using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RadialMenuHelper : MonoBehaviour {

    public Image blackout;
    public Image radialLoading;

    public void SetDeltaTime(float lerp)
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);

        if(lerp > 1f)
        {
            SetBlackoutLerp(1f);
            radialLoading.fillAmount = 1f;
        }
        else
        {
            SetBlackoutLerp(lerp);
            radialLoading.fillAmount = lerp;
        }
    }

    void SetBlackoutLerp(float lerp)
    {
        if (lerp > 0.5f)
            lerp = 0.5f;
        Color targetColor = blackout.color;
        targetColor.a = lerp / 2f;
        blackout.color = targetColor;
    }

    public void ShowRadialLoading(bool show)
    {
        blackout.gameObject.SetActive(show);
        radialLoading.gameObject.SetActive(show);
        gameObject.SetActive(false);
    }

}
