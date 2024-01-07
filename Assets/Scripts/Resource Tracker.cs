using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTracker : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    public int woodCount = 0;
    public int moneyCount = 0;

    public void incWood(int amount)
    {
        woodCount += amount;
        UpdateWoodQuests();
    }


    public bool decWood(int amount)
    {
        if (woodCount >= amount)
        {
            woodCount -= amount;
            return true;
        } else
        {
            return false;
        }
    }

    public void incMoney(int amount)
    {
        moneyCount += amount;
    }

    private void UpdateWoodQuests()
    {
        foreach (Quest quest in questManager.activeQuests)
        {
            if (quest.objectivies[0].type == QuestObjectiveType.CollectWood)
            {
                quest.objectivies[0].currentAmount++;
            }
        }
    }
}
