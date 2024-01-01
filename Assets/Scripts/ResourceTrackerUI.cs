using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceTrackerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI woodCount;
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
    }

    void UpdateWoodCount()
    {
        woodCount.gameObject.SetActive(resourceTracker.woodCount != 0);
        woodCount.text = "Wood: " + resourceTracker.woodCount;
    }
}
