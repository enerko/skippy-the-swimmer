using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this to the main UI to enable/disable it during runtime in editor/debug builds
public class DebugHUDDisabler : MonoBehaviour
{
    private Canvas _canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            _canvas.enabled = !_canvas.enabled;
        }
    }
}
