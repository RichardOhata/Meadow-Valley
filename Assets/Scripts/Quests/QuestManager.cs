using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] public List<Quest> quests = new List<Quest>();
    [SerializeField] public List<Quest> activeQuests = new List<Quest>();
    [SerializeField] private GameObject journal;
    [SerializeField] private GameObject journalContent;
    [SerializeField] private GameObject questEntryPrefab;
    private bool journalIsOpen = false;

    // Update is called once per frame
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
        questTitle.text = quest.questName;
    }
    private void OpenJournal()
    {  
        journalIsOpen = !journalIsOpen;
        if (journalIsOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UpdateJournal();
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        journal.SetActive(journalIsOpen);

    }

    public void AssignQuest()
    {
        int randomIndex = Random.Range(0, quests.Count);
        activeQuests.Add(quests[randomIndex]);
    }
}
