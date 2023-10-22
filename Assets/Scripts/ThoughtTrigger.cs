using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThoughtTrigger : MonoBehaviour
{
    public bool repeatable = false;
    public string thought;
    
    private GameObject _thoughtBubble;
    private TextMeshProUGUI _thoughtText;
    private GameObject _playerMesh;
    private const float Duration = 3;
    private readonly Vector3 Offset = new Vector3(60, 30, 0);
    private static GameObject s_CurrentThought;

    // Start is called before the first frame update
    void Start()
    {
        _thoughtBubble = GameObject.Find("/Game UI/Thought Bubble");
        _thoughtText = _thoughtBubble.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        _playerMesh = GameObject.Find("/SkippyController/Skippy");
    }

    // Update is called once per frame
    void Update()
    {
        if (s_CurrentThought != gameObject) return;
        _thoughtBubble.SetActive(true);
        _thoughtBubble.GetComponent<RectTransform>().anchoredPosition = CameraMain.CustomWorldToScreenPoint(_playerMesh.transform.position) + Offset;
    }

    // May assume that other is the player if include/exclude options are set properly
    void OnTriggerEnter(Collider other) {
        if (s_CurrentThought == gameObject) return;  // do nothing if already showing

        s_CurrentThought = gameObject;

        // show thought
        StartCoroutine(ShowThought());
    }

    private IEnumerator ShowThought() {
        _thoughtText.text = thought;
        _thoughtBubble.SetActive(true);

        yield return new WaitForSeconds(Duration);

        s_CurrentThought = null;
        _thoughtBubble.SetActive(false);

        if (!repeatable) {
            Destroy(gameObject);
        }
    }
}
