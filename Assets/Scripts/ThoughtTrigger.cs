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
    private bool _showing = false;
    private GameObject _playerMesh;
    private const float Duration = 5;
    private readonly Vector3 Offset = new Vector3(0, 0, 0);

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
        _thoughtBubble.SetActive(_showing);  // set visible or not
        _thoughtBubble.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(_playerMesh.transform.position) + Offset;
    }

    void OnTriggerEnter(Collider other) {
        if (_showing) return;  // do nothing if already showing

        // show thought
        StartCoroutine(ShowThought());
    }

    private IEnumerator ShowThought() {
        _showing = true;
        _thoughtText.text = thought;

        yield return new WaitForSeconds(Duration);

        _showing = false;

        if (!repeatable) {
            _thoughtBubble.SetActive(false);
            Destroy(gameObject);
        }
    }
}
