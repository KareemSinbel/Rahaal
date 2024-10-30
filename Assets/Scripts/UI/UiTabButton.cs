using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiTabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public SettingsTabsHandler tabsHandler;
    public RawImage targetImage;

    private Vector3 originalScale;
    public float zoomFactor = 1.4f;
    public float animationDuration = 0.2f;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void Start()
    {
        if (targetImage == null)
        {
            targetImage = GetComponentInChildren<RawImage>();     
        }
        tabsHandler.Subscribe(this);
    }


    public void OnPointerClick(PointerEventData eventData)
    {    
        tabsHandler.OnTabSelected(this);       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabsHandler.OnTabEnter(this);
        StopAllCoroutines();
        StartCoroutine(AnimateScale(originalScale * zoomFactor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabsHandler.OnTabExit(this);
        StopAllCoroutines();
        StartCoroutine(AnimateScale(originalScale)); 
    }


    private System.Collections.IEnumerator AnimateScale(Vector3 targetScale)
    {
        Vector3 initialScale = transform.localScale;
        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / animationDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
