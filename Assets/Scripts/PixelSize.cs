using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelSize : MonoBehaviour
{
    [SerializeField]
    public Material pixelMaterial;

    // Start is called before the first frame update
    void Start()
    {
       UpdatePixelSize();
    }

    public void UpdatePixelSize() {
        pixelMaterial.SetFloat("_Pixel_Size", PlayerPrefs.GetFloat("Pixel Size", 2));
    }
}
