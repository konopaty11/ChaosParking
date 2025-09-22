using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionUI : MonoBehaviour
{
    [SerializeField] protected bool changeAlpha;
    [SerializeField] protected bool changeScale;
    [SerializeField] protected float delay;

    protected CanvasGroup canvasGroup;  
    protected RectTransform rectTransform;

    public abstract void StartActionUI();
}
