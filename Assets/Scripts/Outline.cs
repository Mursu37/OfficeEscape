using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Outline : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    private bool _currentlyOutlined;

    public void AddOutline()
    {
        
    }
    
    public void RemoveOutline()
    {
        if (!_currentlyOutlined) return;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            var currTransform = transform.GetChild(i);
        }

    }

    private void ChangeLayerRecursively(Transform t, LayerMask l)
    {
        
    }
}
