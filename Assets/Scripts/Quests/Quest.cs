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
}

[System.Serializable]
public class QuestObjective
{
    public string objectiveDescription;
    public int currentAmount;
    public int targetAmount;
    public bool isComplete => currentAmount >= targetAmount;
}
