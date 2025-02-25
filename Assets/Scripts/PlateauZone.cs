using UnityEngine;
using UnityEngine.EventSystems;

public class PlateauZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableCard card = eventData.pointerDrag.GetComponent<DraggableCard>();
        if (card != null)
        {
            card.transform.SetParent(transform);
            card.SetOnBoard(true);
        }
    }
}