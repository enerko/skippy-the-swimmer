using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Objectives : MonoBehaviour
{
    private string _objective = "";

    public TextMeshProUGUI textUI;

    private RectTransform _rect;
    private Image _background;

    private Color32 _grey = new Color32(53, 81, 100, 200);
    private Color32 _green = new Color32(126, 173, 136, 200);
    private float _hiddenPosY = 20;
    private float _showingPosY = -20;


    // Start is called before the first frame update
    void Start()
    {
        _rect = gameObject.GetComponent<RectTransform>();
        _rect.anchoredPosition = new Vector3(0, _hiddenPosY, 0);
        _background = gameObject.GetComponent<Image>();

        UpdateObjective(_objective);
      
    }

    public void UpdateObjective(string newObjective) 
    {
        StartCoroutine(CompleteObjective(newObjective));
    }

    private IEnumerator CompleteObjective(string newObjective)
    {
        if (_objective != "") 
        {
            // hide completed objective

            _background.color = _green;
            yield return new WaitForSeconds(0.3f);
            _background.color = _grey;
            yield return new WaitForSeconds(0.2f);
            _background.color = _green;
            yield return new WaitForSeconds(0.2f);
            
            _rect.anchoredPosition = new Vector3(0, _showingPosY - 4, 0);
            yield return new WaitForSeconds(0.005f);


            for (float i = 1; _showingPosY + i < _hiddenPosY; i += i*2) 
            {
                _rect.anchoredPosition = new Vector3(0, _showingPosY + i, 0);
                 yield return new WaitForSeconds(Mathf.Abs(0.01f/i));
            }

            _rect.anchoredPosition = new Vector3(0, _hiddenPosY, 0);
        }
       
        textUI.text = newObjective;
        _background.color = _grey;
        _rect.anchoredPosition = new Vector3(0, _hiddenPosY, 0);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);

        if (newObjective != "")
        {
            // show new objective

            for (float i = 1; _hiddenPosY - i > _showingPosY; i += i*2) 
            {
                _rect.anchoredPosition = new Vector3(0, _hiddenPosY - i, 0);
                 yield return new WaitForSeconds(Mathf.Abs(0.02f/i));
            }

             _rect.anchoredPosition = new Vector3(0, _showingPosY - 4, 0);
            yield return new WaitForSeconds(0.01f);
            
            _rect.anchoredPosition = new Vector3(0, _showingPosY, 0);
        }

        _objective = newObjective;
    }

    public void HideObjectives()
    {
        gameObject.transform.localScale = Vector3.zero;
    }

    public void ShowObjectives()
    {
        gameObject.transform.localScale = Vector3.one;
    }
}
