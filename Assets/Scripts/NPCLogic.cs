using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLogic : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField, Min(1)] private float hitRange = 10;
    [SerializeField] private LayerMask NPClayerMask;
    [SerializeField] public GameObject questManager;

    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
            hit.collider.GetComponent<Outline>().OutlineWidth = 0;
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, NPClayerMask))
        {
            if (hit.collider != null)
            {
                hit.collider.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
                hit.collider.GetComponent<Outline>().OutlineWidth = 3;
            

                if (Input.GetKeyDown(KeyCode.E))
                {

                    questManager.GetComponent<QuestManager>().AssignQuest();
                }
            }
        }
    
}
}
