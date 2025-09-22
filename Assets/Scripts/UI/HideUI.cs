using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : ActionUI
{
    [SerializeField] float duration = 0.4f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public override void StartActionUI()
    {
        gameObject.SetActive(true);
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f; 
        float startAlpha = canvasGroup.alpha; 
        Vector3 startScale = rectTransform.localScale; 
        Vector3 targetScale = Vector3.zero;  

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration; 

            if (changeAlpha)
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, progress);

            if (changeScale)
                rectTransform.localScale = Vector3.Lerp(startScale, targetScale, progress);

            yield return null; 
        }

        if (changeAlpha)
            canvasGroup.alpha = 0f;
        if (changeScale)
            rectTransform.localScale = targetScale;
        gameObject.SetActive(false);

        canvasGroup.blocksRaycasts = false;
    }
}
