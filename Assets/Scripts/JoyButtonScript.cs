using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButtonScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool pressed = false;


    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
