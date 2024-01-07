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
            newQuest.RandomizeReward(100, 500);
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
        Destroy(questEntry);
    }
}
