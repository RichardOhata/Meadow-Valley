using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPickUpLogic : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;


    [SerializeField]
    private Transform playerCameraTransform;


    [SerializeField]
    private GameObject pickUpUI;


    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    public GameObject inHandItem;

    private RaycastHit hit;

    private void Update()
    {

            Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
            if (hit.collider != null)
            {
                hit.collider.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
                hit.collider.GetComponent<Outline>().OutlineWidth = 0;
                pickUpUI.SetActive(false);
            }

                
            if (inHandItem != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {

                    if (hit.collider != null)
                    {
                    if (inHandItem.GetComponent<Animator>() != null)
                    {
                        inHandItem.GetComponent<Animator>().enabled = false;
                    }
                    inHandItem.GetComponent<MeshCollider>().enabled = true;
                        inHandItem.transform.SetParent(null);
                        inHandItem = null;
                       
                        Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.isKinematic = false;
                        }
                    }
                }
                return;
            }

            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask))
            {
                hit.collider.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
                hit.collider.GetComponent<Outline>().OutlineWidth = 3;
                pickUpUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
            
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                inHandItem = hit.collider.gameObject;
                inHandItem.transform.position = Vector3.zero;
                inHandItem.transform.rotation = Quaternion.identity;
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                if (inHandItem.GetComponent<Animator>() != null)
                {
                    inHandItem.GetComponent<Animator>().enabled = true;
                }

                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.constraints = RigidbodyConstraints.None;

                }
                }
            }
    }
}
