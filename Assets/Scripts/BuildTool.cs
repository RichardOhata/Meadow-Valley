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
    [SerializeField] private GameObject player;
    private ResourceTracker resourceTracker;
    public bool _deleteModeEnabled;

    private Camera _camera;

    [SerializeField] public Building _spawnedBuilding;
    //private Building _targetBuilding;
    private Quaternion _lastRotation;
    void Start()
    {
        resourceTracker = player.GetComponent<ResourceTracker>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        toggleBuildMode();

        if (_deleteModeEnabled)
        {
            //deleteBuildLogic();
        }
        else
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
        //if (_targetBuilding != null && _targetBuilding.FlaggedForDelete)
        //{
        //    _targetBuilding.RemoveDeleteFlag();
        //    _targetBuilding = null;
        //}
        if (_spawnedBuilding == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _spawnedBuilding.transform.Rotate(0, 90, 0);
            _lastRotation = _spawnedBuilding.transform.rotation;
        }

        if (!IsRayHittingSomething(_buildModeLayerMask, out RaycastHit hitInfo))
        {
            _spawnedBuilding.UpdateMaterial(_buildingMatNegative);
        }
        else
        {
            _spawnedBuilding.UpdateMaterial(_buildingMatPositive);
            _spawnedBuilding.transform.position = hitInfo.point;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (resourceTracker.decWood(5))
            {
                _spawnedBuilding.RemoveDeleteFlag();
                Building placedBuilding = Instantiate(_spawnedBuilding, hitInfo.point, _lastRotation);
                placedBuilding.PlaceBuilding();
            }
        
        }
    }

    //private void deleteBuildLogic()
    //{
    //    if (IsRayHittingSomething(_deleteModeLayerMask, out RaycastHit hitInfo))
    //    {
    //        var detectedBuilding = hitInfo.collider.gameObject.GetComponentInParent<Building>();

    //        if (detectedBuilding == null) return;

    //        if (_targetBuilding == null) _targetBuilding = detectedBuilding;

    //        if (detectedBuilding != _targetBuilding && _targetBuilding.FlaggedForDelete)
    //        {
    //            _targetBuilding.RemoveDeleteFlag();
    //            _targetBuilding = detectedBuilding;
    //        }

    //        if (detectedBuilding == _targetBuilding && !_targetBuilding.FlaggedForDelete)
    //        {
    //            _targetBuilding.FlagForDelete(_buildingMatNegative);
    //        }

    //        if (Input.GetKeyDown(KeyCode.Mouse0))
    //        {
    //            Destroy(_targetBuilding.gameObject);
    //            _targetBuilding = null;
    //        }
    //    }
    //    else
    //    {
    //        if (_targetBuilding != null && _targetBuilding.FlaggedForDelete)
    //        {
    //            _targetBuilding.RemoveDeleteFlag();
    //            _targetBuilding = null;
    //        }
    //    }
    //}

    private void toggleBuildMode()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            _deleteModeEnabled = !_deleteModeEnabled;
        }
    }


}
