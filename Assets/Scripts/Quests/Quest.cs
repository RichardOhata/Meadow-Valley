using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Quest", menuName ="Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string questDescription;

    public QuestObjective[] objectivies;

    public int reward;

    public void RandomizeReward(int min, int max)
    {
        reward = Random.Range(min, max);
    }

    public virtual void InitializeQuest()
    {
        Debug.Log("Base Quest Initalization");
    }
}

[System.Serializable]
public class QuestObjective
{
    public QuestObjectiveType type;
    public string objectiveDescription;
    public int currentAmount;
    public int targetAmount;
    public bool isComplete => currentAmount >= targetAmount;
    public void resetAmount()
    {
        currentAmount = 0;
    }
}

public enum QuestObjectiveType
{
    Nothing,
    CollectWood,
    CollectStone,
    GlowingBlocks,
    Fishing
}