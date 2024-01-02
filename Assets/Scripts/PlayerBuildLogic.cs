using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBuildLogic : MonoBehaviour
{
    ResourceTracker resourceTracker;
    [SerializeField] private TextMeshProUGUI craftPrompt;
    [SerializeField] private GameObject buildTool;
    [SerializeField] Building campFire;
    private bool craftPromptShown = false;
    private bool buildToolActive = false;
    private void Start()
    {
        resourceTracker= gameObject.GetComponent<ResourceTracker>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (resourceTracker.woodCount >= 5 && !craftPromptShown)
        {
            StartCoroutine(showCraftPrompt());
            craftPromptShown = true;
        }
        
        if (Input.GetKeyDown(KeyCode.F)) {
            ToggleBuildTool();
            buildTool.SetActive(buildToolActive);
            buildTool.GetComponent<BuildTool>()._spawnedBuilding = campFire;
        }
    }

    private IEnumerator showCraftPrompt()
    {
        craftPrompt.gameObject.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        Destroy(craftPrompt.gameObject);

    }

    private void ToggleBuildTool()
    {
        buildToolActive = !buildToolActive;
    }
}
