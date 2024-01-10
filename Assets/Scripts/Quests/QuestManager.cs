using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] public List<Quest> quests = new List<Quest>();
    [SerializeField] public List<Quest> activeQuests = new List<Quest>();
    [SerializeField] private GameObject journal;
    [SerializeField] private GameObject journalContent;
    [SerializeField] private GameObject questEntryPrefab;
    [SerializeField] private GameObject questDescPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject glowingCubeSpawnArea;
    [SerializeField] private GameObject glowingCubePrefab;
    private bool journalIsOpen = false;

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenJournal();
        }
    }

    private void UpdateJournal()
    {
        foreach (Transform child in journalContent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Quest quest in activeQuests)
        {
            CreateQuestEntry(quest);
        }
    }

    private void CreateQuestEntry(Quest quest)
    {
        GameObject questEntry = Instantiate(questEntryPrefab, journalContent.transform);
        TextMeshProUGUI questTitle = questEntry.GetComponentInChildren<TextMeshProUGUI>();
        Button button = questEntry.GetComponent<Button>();
      
        questTitle.text = quest.questName;
        if (quest.objectivies[0].isComplete)
        {
            button.GetComponent<Image>().color = Color.green;
            button.onClick.AddListener(() => DeleteQuest(quest, questEntry));
        } else
        {
            button.onClick.AddListener(() => ShowQuestDetails(quest));
        }
    }
    private void OpenJournal()
    {  
        journalIsOpen = !journalIsOpen;
        if (journalIsOpen)
        {
            player.GetComponent<FirstPersonController>().cameraCanMove = false;
            player.GetComponent<FirstPersonController>().crosshair = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UpdateJournal();
        } else
        {
            player.GetComponent<FirstPersonController>().cameraCanMove = true;
            player.GetComponent<FirstPersonController>().crosshair = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        journal.SetActive(journalIsOpen);
    }

    public void AssignQuest()
    {
        int randomIndex = Random.Range(0, quests.Count);
        if (!activeQuests.Contains(quests[randomIndex]))
        {
            Quest newQuest = quests[randomIndex];
            if (newQuest.objectivies[0].type == QuestObjectiveType.CollectWood)
            {
                newQuest.RandomizeReward(100, 500);
            } else if(newQuest.objectivies[0].type == QuestObjectiveType.GlowingBlocks)
            {
                newQuest.RandomizeReward(1000, 1500);
                SpawnGlowingCubes();
            } else if (newQuest.objectivies[0].type == QuestObjectiveType.Fishing)
            {
                newQuest.RandomizeReward(50, 100);
                SpawnFish();
            }
          
            activeQuests.Add(newQuest);
        }
        else if (activeQuests.Count == quests.Count)
        {
            Debug.Log("All Quests exhausted");
            return;
        } else
        {
            AssignQuest();
        }
    }

    void ShowQuestDetails(Quest quest)
    {
        foreach (Transform child in journalContent.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject questDesc = Instantiate(questDescPrefab, journalContent.transform);
        TextMeshProUGUI[] texts = questDesc.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI textUI in texts)
        {
            if (textUI.name == "QuestTitle")
            {
                textUI.text = quest.questName;
            }
            if (textUI.name == "QuestDesc")
            {
                textUI.text = quest.questDescription;
            }
            if (textUI.name == "QuestReward")
            {
                textUI.text = "Reward: " + quest.reward.ToString();
            }
            if (textUI.name == "Progress")
            {
                textUI.text = "Progress: " + quest.objectivies[0].currentAmount.ToString() + "/" + quest.objectivies[0].targetAmount.ToString();
            }
        }
    }

    void DeleteQuest(Quest quest, GameObject questEntry)
    {
        quest.objectivies[0].resetAmount();
        player.GetComponent<ResourceTracker>().incMoney(quest.reward);
        activeQuests.Remove(quest);
        Destroy(questEntry);
    }

    // Custom Quest Methods
    private void SpawnGlowingCubes()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPosition = GetRandomPositionInSpawnArea();
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            Instantiate(glowingCubePrefab, randomPosition, randomRotation);
        }

    }
    Vector3 GetRandomPositionInSpawnArea()
    {
        Bounds colliderBounds = glowingCubeSpawnArea.GetComponent<BoxCollider>().bounds;

        float randomX = Random.Range(colliderBounds.min.x, colliderBounds.max.x);
        float randomZ = Random.Range(colliderBounds.min.z, colliderBounds.max.z);

        return new Vector3(randomX, colliderBounds.center.y, randomZ);
    }

    private void SpawnFish()
    {

    }
}
