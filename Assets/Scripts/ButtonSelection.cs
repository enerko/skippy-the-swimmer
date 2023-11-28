using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    private float xOffset = 30;
    private GameObject _pointer;
    private Button _button;
    
    //private bool isGamepad = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _pointer = GameObject.Find("Pointer");
        _button = GetComponent<Button>();
        // if (ControllerTypeHandler.currentController == ControllerTypeHandler.ControllerType.Gamepad)
        // {
        //     isGamepad = true;
        // }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _button.Select();  // when the pointer enters this button, select it
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(gameObject.name);
        _pointer.transform.position = new Vector2(transform.position.x - (transform.position.x / 3) + xOffset, transform.position.y);
    }
}
