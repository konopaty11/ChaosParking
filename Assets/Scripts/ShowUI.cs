using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : ActionUI
{
    [SerializeField] float duration = 0.2f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public override void StartActionUI()
    {
        gameObject.SetActive(true);
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds(delay);

        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0;
        Vector3 startScale = new Vector3(0.1f, 0.1f, 0.1f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            if (changeAlpha)
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);
            if (changeScale)
                rectTransform.localScale = Vector3.Lerp(startScale, Vector3.one, progress);

            yield return null;
        }

        if (changeAlpha)
            canvasGroup.alpha = 1;
        if (changeScale)
            rectTransform.localScale = Vector3.one;

        canvasGroup.blocksRaycasts = true;
    }
}
