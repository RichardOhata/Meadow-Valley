using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer _renderer;
    private Material _defaultMaterial;

    private bool _flaggedForDelete;
    public bool FlaggedForDelete => _flaggedForDelete;

    private void Awake()
    {
        
        _renderer = GetComponentInChildren<Renderer>();
        if (_renderer)
        {
            Debug.Log(_renderer.material);
            _defaultMaterial = _renderer.material;
        }
    }

    public void UpdateMaterial(Material newMaterial)
    {
        if (_renderer.material != newMaterial)
        {
            _renderer.material = newMaterial;   
        }
    }

    public void PlaceBuilding()
    {
        UpdateMaterial(_defaultMaterial);
    }

    public void FlagForDelete(Material deleteMat)
    {
        UpdateMaterial(deleteMat);
        _flaggedForDelete = true;
    }

    public void RemoveDeleteFlag()
    {
        UpdateMaterial(_defaultMaterial);
        _flaggedForDelete = false;
    }
}
