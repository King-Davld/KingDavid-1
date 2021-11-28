﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public static QuestManager instance;

    [SerializeField] string[] questNames;
    [SerializeField] bool[] questMarkersCompleted;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        questMarkersCompleted = new bool[questNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print(CheckIfComplete("Kill Dragon"));
            MarkQuestComplete("Steal the Gem");
            MarkQuestInComplete("Take Monster Soul");
        }
    }

    public int GetQuestNumber(string questToFind)
    {
        for(int i=0; i < questNames.Length; i++)
        {
            if (questNames[i] == questToFind)
            {
                return i;
            }
        }
        Debug.LogWarning("Quest " + questToFind +" does no exist");
        return 0;
    }

    public bool CheckIfComplete(string questToCheck)
    {
        int questNumberToCheck = GetQuestNumber(questToCheck);
        if(questNumberToCheck != 0)
        {
            return questMarkersCompleted[questNumberToCheck];
        }
        return false;
    }

    public void UpdateQuestObjects()
    {
        QuestObject[] questObjects = FindObjectsOfType<QuestObject>();
        if (questObjects.Length > 0)
        {
            foreach (QuestObject questObject in questObjects)
            {
                questObject.CheckForCompletion();
            }
        }
    }

    public void MarkQuestComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkersCompleted[questNumberToCheck] = true;

        UpdateQuestObjects();
    }

    public void MarkQuestInComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkersCompleted[questNumberToCheck] = false;

        UpdateQuestObjects();

    }

}
