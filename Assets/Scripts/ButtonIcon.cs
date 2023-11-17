using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIcon : MonoBehaviour
{

    public ControllerTypeHandler.PromptKey promptKeyController;  
    public ControllerTypeHandler.PromptKey promptKeyKeyboard;

    // Start is called before the first frame update
    void Start()
    {
        Image promptIcon = gameObject.GetComponent<Image>();
        if (ControllerTypeHandler.currentController == ControllerTypeHandler.ControllerType.Gamepad) {
            promptIcon.sprite = ControllerTypeHandler.GetIcon(promptKeyController);
        } else {
            promptIcon.sprite =  ControllerTypeHandler.GetIcon(promptKeyKeyboard);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        Image promptIcon = gameObject.GetComponent<Image>();
        if (ControllerTypeHandler.currentController == ControllerTypeHandler.ControllerType.Gamepad) {
            promptIcon.sprite = ControllerTypeHandler.GetIcon(promptKeyController);
        } else {
            promptIcon.sprite =  ControllerTypeHandler.GetIcon(promptKeyKeyboard);
        } 
    }
}
