using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class PlayerClickController : MonoBehaviour, IPointerClickHandler
{
    public event UnityAction OnClick;
    public void OnPointerClick(PointerEventData eventData)
    {        
        OnClick?.Invoke();
    }
}