
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchHolder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction OnPionterDownEvent;
    public UnityAction<float> OnPointerDragEvent;
    public UnityAction OnPointerUpEvent;

    private Slider uiSlider;

    private void Awake()
    {
        uiSlider = GetComponent<Slider>();
        uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPionterDownEvent != null)
            OnPionterDownEvent.Invoke();

        if (OnPointerDragEvent != null)
            OnPointerDragEvent.Invoke(uiSlider.value);
    }
    
    private void OnSliderValueChanged( float value)
    {
        if (OnPointerDragEvent != null)
            OnPointerDragEvent.Invoke(value);
     
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnPointerUpEvent != null)
            OnPointerUpEvent.Invoke();

        //reset the slider
        uiSlider.value = 0f;
    }
    public void OnDestory()
    {
        //remove all listeners: to avoid mmemory leaks
        uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
