using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField] ResourceTracker resourceTracker;

    public AudioSource woodPickUpSFX;
    private void Update()
    {
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
            hit.collider.GetComponent<Outline>().OutlineWidth = 0;
            pickUpUI.SetActive(false);
        }

        DropInHandItem();


        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask))
        {
            hit.collider.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
            hit.collider.GetComponent<Outline>().OutlineWidth = 3;
            //UpdateUI();
            pickUpUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Pickup"))
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
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Resource"))
                {
                    if (hit.collider.gameObject.tag == "Wood")
                    {
                        resourceTracker.incWood(1);
                        woodPickUpSFX.Play(1);
                    } else if (hit.collider.gameObject.tag == "Cube")
                    {
               
                        resourceTracker.incCube();
                    } else if (hit.collider.gameObject.tag == "Fish")
                    {
                        resourceTracker.incFish();
                    }

                    Destroy(hit.collider.gameObject);
                    pickUpUI.SetActive(false);
                }
            }
        }
    }

    void DropInHandItem()
    {

        if (inHandItem != null && Input.GetKeyDown(KeyCode.Q))
        {
            if (inHandItem.GetComponent<Animator>() != null)
            {
                inHandItem.GetComponent<Animator>().enabled = false;
            }
            inHandItem.GetComponent<MeshCollider>().enabled = true;
            inHandItem.transform.SetParent(null);
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
            inHandItem = null;


            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    private void UpdateUI()
    {
        pickUpUI.GetComponentInChildren<TextMeshProUGUI>().text = "Press 'E' to pick up";
    }
}
