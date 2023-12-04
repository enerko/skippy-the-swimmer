using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler, IDeselectHandler
{
    public float xOffset = 30;
    private GameObject pointer;
    //private bool isGamepad = false;
    private TextMeshProUGUI button;
    private Color origColor;
    public Color selectedColor;
    public bool highlight;
    public AudioClip btnSound;
    public bool showPointer = true;  // whether the pointer should be showed (really only used for the menu buttons)

    private Button _button;
    
    // Start is called before the first frame update
    void Start()
    {
        if (highlight)
        {
            button = GetComponentInChildren<TextMeshProUGUI>();
            origColor = button.color;
        }

        if (showPointer)
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
        CameraMain.PlaySFX(btnSound);
        if (highlight)
        {
            button.color = selectedColor;
            return;
        }

        if (!showPointer)
            return;

        pointer.SetActive(true);
        pointer.transform.position = new Vector2(pointer.transform.position.x, transform.position.y);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        OnDeselect(eventData);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // wtf???
        if (EventSystem.current == null) {
            return;
        }

        if (!EventSystem.current.alreadySelecting && (EventSystem.current.currentSelectedGameObject == gameObject))
            EventSystem.current.SetSelectedGameObject(null);

        pointer?.SetActive(false);
        if (highlight)
        {
            //Debug.Log(button);
            button.color = origColor;
            return;
        }
    }

    public void ForceDeselect() {
        OnDeselect(null);
    }
}
