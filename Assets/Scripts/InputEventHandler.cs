using UnityEngine;
using UnityEngine.EventSystems;

abstract public class InputEventHandler : MonoBehaviour
, IDeselectHandler
, IDragHandler
, IDropHandler
, IMoveHandler
, IPointerClickHandler
, IPointerDownHandler
, IPointerEnterHandler
, IPointerExitHandler
, IPointerUpHandler
, IScrollHandler
, ISelectHandler
, IUpdateSelectedHandler
{
    public void OnPointerEnter(PointerEventData eventData) { Debug.Log("OnPointerEnter"); }
    public void OnPointerDown(PointerEventData eventData) { Debug.Log("OnPointerDown"); }
    public void OnDrag(PointerEventData eventData) { Debug.Log("OnDrag"); }
    public void OnPointerUp(PointerEventData eventData) { Debug.Log("OnPointerUp"); }
    public void OnPointerExit(PointerEventData eventData) { Debug.Log("OnPointerExit"); }

    public void OnPointerClick(PointerEventData eventData) { Debug.Log("OnPointerClick"); }
    public void OnScroll(PointerEventData eventData) { Debug.Log("OnScroll"); }

    public void OnDeselect(BaseEventData eventData) { Debug.Log("OnDeselect"); }
    public void OnDrop(PointerEventData eventData) { Debug.Log("OnDrop"); }
    public void OnMove(AxisEventData eventData) { Debug.Log("OnMove"); }
    public void OnSelect(BaseEventData eventData) { Debug.Log("OnSelect"); }
    public void OnUpdateSelected(BaseEventData eventData) { Debug.Log("OnUpdateSelected"); }
}
