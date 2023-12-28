using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI woodCount;
    [SerializeField] GameObject player;
    PlayerPickUpLogic playerPickUpLogic;

    private void Start()
    {
        playerPickUpLogic = player.GetComponent<PlayerPickUpLogic>();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateWoodCount();
    }

    void UpdateWoodCount()
    {
        if (playerPickUpLogic.woodCount != 0)
        {
            woodCount.gameObject.SetActive(true);
        }
        woodCount.text = "Wood: " + playerPickUpLogic.woodCount;
    }
}
