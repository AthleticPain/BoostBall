using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    [SerializeField] Material[] materials;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ColorChanger>())
        {
            InvertColor();
        }
    }

    void InvertColor()
    {
        foreach(Material material in materials)
        {
            material.color = new Color(1 - material.color.r, 1 - material.color.g, 1 - material.color.b);
        }
    }
}
