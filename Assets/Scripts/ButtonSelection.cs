using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler, IDeselectHandler
{
    private float xOffset = 30;
    private GameObject pointer;
    //private bool isGamepad = false;
    private TextMeshProUGUI button;
    private Color origColor;
    public Color selectedColor;
    public bool highlight;

    private Button _button;
    
    // Start is called before the first frame update
    void Start()
    {
        if (highlight)
        {
            button = GetComponentInChildren<TextMeshProUGUI>();
            origColor = button.color;
        }
        pointer = GameObject.Find("Pointer");
        _button = GetComponent<Button>();
        // if (ControllerTypeHandler.currentController == ControllerTypeHandler.ControllerType.Gamepad)
        // {
        //     isGamepad = true;
        // }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _button.Select();
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
