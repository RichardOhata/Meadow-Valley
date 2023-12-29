using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceTrackerUI : MonoBehaviour
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
        woodCount.gameObject.SetActive(playerPickUpLogic.woodCount != 0);
        woodCount.text = "Wood: " + playerPickUpLogic.woodCount;
    }
}
