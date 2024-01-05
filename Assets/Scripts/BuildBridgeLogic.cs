using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeLogic : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;

    [SerializeField, Min(1)] private float hitRange = 10;

    [SerializeField] private GameObject buildBridgeUI;
    [SerializeField] private GameObject bridge;
    [SerializeField] private ResourceTracker resourceTracker;
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.green);
        if (hit.collider != null && hit.collider.gameObject.name == "BuildBridgeSign")
        {
            hit.collider.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
            hit.collider.GetComponent<Outline>().OutlineWidth = 0;
            buildBridgeUI.SetActive(false);
        }

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange))
        {
            if (hit.collider != null && hit.collider.gameObject.name == "BuildBridgeSign")
            {
                hit.collider.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
                hit.collider.GetComponent<Outline>().OutlineWidth = 3;
                buildBridgeUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                  
                    if (resourceTracker.decWood(50))
                    {
                        bridge.SetActive(true);
                        buildBridgeUI.SetActive(false);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
