using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelection : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    private float xOffset = 30;
    private GameObject pointer;
    private bool isGamepad = false;
    
    // Start is called before the first frame update
    void Start()
    {
        pointer = GameObject.Find("Pointer");
        if (ControllerTypeHandler.currentController == ControllerTypeHandler.ControllerType.Gamepad)
        {
            isGamepad = true;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        pointer.transform.position = new Vector2(transform.position.x - (transform.position.x / 3) + xOffset, transform.position.y);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(gameObject.name);
        pointer.transform.position = new Vector2(transform.position.x - (transform.position.x / 3) + xOffset, transform.position.y);
    }
}
