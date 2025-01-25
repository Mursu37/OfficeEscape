using UnityEngine;

public class Outline : MonoBehaviour
{
    private bool _currentlyOutlined;
    private int _originalLayer;

    private void Start()
    {
        _originalLayer = gameObject.layer;
    }

    public void AddOutline()
    {
        if (_currentlyOutlined) return;
        ChangeLayerRecursively(transform, LayerMask.NameToLayer("Outlined"));
        _currentlyOutlined = true;
    }
    
    public void RemoveOutline()
    {
        if (!_currentlyOutlined) return;
        ChangeLayerRecursively(transform, _originalLayer);
        _currentlyOutlined = false;
    }

    private void ChangeLayerRecursively(Transform t, int l)
    {
        t.gameObject.layer = l;
        for (int i = 0; i < t.childCount; i++)
        {
            var currTransform = transform.GetChild(i);
            ChangeLayerRecursively(currTransform, l);
        }
    }
}
