using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonZoomAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private Vector3 originalPos;
    private GameObject buttonImage;
    public float zoomFactor = 1.2f;
    public float moveDistance = 10f;
    public float animationDuration = 0.2f;

    private void Awake()
    {
        buttonImage = transform.GetChild(0).gameObject;
        originalScale = buttonImage.transform.localScale;
        originalPos = buttonImage.transform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateScale(originalScale * zoomFactor));
        StartCoroutine(AnimateMovement(originalPos + new Vector3(moveDistance, 0, 0)));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateScale(originalScale)); 
        StartCoroutine(AnimateMovement(originalPos));
    }

    private System.Collections.IEnumerator AnimateScale(Vector3 targetScale)
    {
        Vector3 initialScale = buttonImage.transform.localScale;

        float elapsedTime = 0;
        while (elapsedTime < animationDuration)
        {
            buttonImage.transform.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / animationDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        buttonImage.transform.localScale = targetScale;
    }

    private System.Collections.IEnumerator AnimateMovement(Vector3 targetPosition)
    {
        Vector3 initialPosition = buttonImage.transform.localPosition;
        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            buttonImage.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / animationDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        buttonImage.transform.localPosition = targetPosition;
    }
}
