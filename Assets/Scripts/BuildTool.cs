using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTool : MonoBehaviour
{

    [SerializeField] private float _rayDistance;
    [SerializeField] private LayerMask _buildModeLayerMask;
    [SerializeField] private LayerMask _deleteModeLayerMask;
    [SerializeField] private int _defaultLayerInt = 8;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private Material _buildingMatPositive;
    [SerializeField] private Material _buildingMatNegative;

    public bool _deleteModeEnabled;

    private Camera _camera;

    public GameObject _gameObjectToPosition;

    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        toggleBuildMode();

        if (_deleteModeEnabled)
        {
            deleteBuildLogic();
        } else
        {
            buildLogic();
        }
    }

    private bool IsRayHittingSomething(LayerMask layerMask, out RaycastHit hitInfo)
    {
        var ray = new Ray(_rayOrigin.position, _camera.transform.forward * _rayDistance);
        return Physics.Raycast(ray, out hitInfo, _rayDistance, layerMask);
    }

    private void buildLogic()
    {
        if (_gameObjectToPosition == null || !IsRayHittingSomething(_buildModeLayerMask, out RaycastHit hitInfo))
        {
            return;
        }

        _gameObjectToPosition.transform.position = hitInfo.point;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(_gameObjectToPosition, hitInfo.point, Quaternion.identity);
        }
    }

    private void deleteBuildLogic()
    {
        if (!IsRayHittingSomething(_deleteModeLayerMask, out RaycastHit hitInfo)) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Destroy(hitInfo.collider.gameObject);
        }
    }

    private void toggleBuildMode()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            _deleteModeEnabled = !_deleteModeEnabled;
        }
    }

  
}
