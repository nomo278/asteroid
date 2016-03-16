using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillHelper : MonoBehaviour {
    public float fillTime = 0.35f;

    public Image image;

    public bool filled = false;

	public void AddOutline()
    {
        StopAllCoroutines();
        StartCoroutine(LerpFill(fillTime, 1f));
        filled = true;
    }

    public void RemoveOutline()
    {
        StopAllCoroutines();
        StartCoroutine(LerpFill(fillTime, 0f));
        filled = false;
    }

    public void ToggleOutline()
    {
        if(filled)
        {
            RemoveOutline();
        }
        else
        {
            AddOutline();
        }
    }

    IEnumerator LerpFill(float totalTime, float fillTarget)
    {
        float elapsedTime = 0f;
        while(elapsedTime < totalTime)
        {
            image.fillAmount = (elapsedTime / totalTime) * fillTarget;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.fillAmount = fillTarget;
    }
}
