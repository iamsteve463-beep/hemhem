using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform bg;
    public RectTransform knob;
    public Vector2 Value { get; private set; }

    public void OnPointerDown(PointerEventData e) => OnDrag(e);
    public void OnPointerUp(PointerEventData e) { Value = Vector2.zero; knob.anchoredPosition = Vector2.zero; }
    public void OnDrag(PointerEventData e)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(bg, e.position, e.pressEventCamera, out pos);
        pos = Vector2.ClampMagnitude(pos, bg.sizeDelta * 0.5f);
        knob.anchoredPosition = pos;
        Value = pos / (bg.sizeDelta * 0.5f);
    }
}
