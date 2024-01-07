using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceTrackerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI woodCount;
    [SerializeField] TextMeshProUGUI moneyCount;
    [SerializeField] GameObject player;
    ResourceTracker resourceTracker;

    private void Start()
    {
       resourceTracker = player.GetComponent<ResourceTracker>();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateWoodCount();
        UpdateMoney();
    }

    private void UpdateWoodCount()
    {
        woodCount.gameObject.SetActive(resourceTracker.woodCount != 0);
        woodCount.text = "Wood: " + resourceTracker.woodCount;
    }

    private void UpdateMoney()
    {
        moneyCount.gameObject.SetActive(resourceTracker.moneyCount != 0);
        moneyCount.text = "Money: " + resourceTracker.moneyCount;
    }
}
