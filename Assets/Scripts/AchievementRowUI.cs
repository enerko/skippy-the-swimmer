using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementRowUI : MonoBehaviour
{
    private Collectible _collectible;
    private bool _collected = false;
    public TextMeshProUGUI description;
    public TextMeshProUGUI distance;
    public GameObject icon;

    // Start is called before the first frame update
    void Start()
    {
        _collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( _collectible && !_collected) {
            SetDistance();
        }
    }

    public void SetInitialValues(Collectible collectible)
    {
        _collectible = collectible;
        description.text = collectible.GetComponent<Collectible>().description;
    }

    public void Collected()
    {
        string text = description.text;
        description.text = "<s>" + text + "</s>";
        description.color = new Color32(89, 105, 140, 120);
        distance.text = "";
        icon.SetActive(true);
        _collected = true;
    }

    public void SetDistance()
    {
        Transform _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        float dist = Vector3.Distance(_playerTransform.position, _collectible.transform.position) / 10;
        distance.text = dist.ToString("F1") + "m";
    }

}
