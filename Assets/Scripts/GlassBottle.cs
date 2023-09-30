using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBottle : MonoBehaviour, IHasLiquid
{
    [SerializeField] GameObject water;

    private void Start()
    {
        water.SetActive(false);
    }
    void IHasLiquid.SpawnLiquid()
    {
        water.SetActive(true);
    }
}
