using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler, IDeselectHandler
{
    private float xOffset = 30;
    private GameObject pointer;
    private bool isGamepad = false;
    private TextMeshProUGUI button;
    private Color origColor;
    public Color32 selectedColor;
    public bool highlight;
    
    // Start is called before the first frame update
    void Start()
    {
        if (highlight)
        {
            button = GetComponentInChildren<TextMeshProUGUI>();
            origColor = button.color;
            return;
        }
        pointer = GameObject.Find("Pointer");
        if (ControllerTypeHandler.currentController == ControllerTypeHandler.ControllerType.Gamepad)
        {
            isGamepad = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highlight)
        {
            Debug.Log(button);
            button.color = selectedColor;
            return;
        }
        pointer.SetActive(true);
        pointer.transform.position = new Vector2(transform.position.x - (transform.position.x / 3) + xOffset, transform.position.y);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (highlight)
        {
            button.color = selectedColor;
            return;
        }
        pointer.SetActive(true);
        pointer.transform.position = new Vector2(transform.position.x - (transform.position.x / 3) + xOffset, transform.position.y);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        pointer?.SetActive(false);
        if (highlight)
        {
            Debug.Log(button);
            button.color = origColor;
            return;
        }

    }

    public void OnDeselect(BaseEventData eventData)
    {
        pointer?.SetActive(false);
        if (highlight)
        {
            Debug.Log(button);
            button.color = origColor;
            return;
        }
    }
}
