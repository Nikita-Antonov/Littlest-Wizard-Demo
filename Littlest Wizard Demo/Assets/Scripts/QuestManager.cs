using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public Text nameText;
    public Text questText;

    private Queue<string> questDialouge;

    [SerializeField] private GameObject levelBoss;

    // Start is called before the first frame update
    void Start()
    {
        questDialouge = new Queue<string>();
    }

    public void StartQuest(Quest quest)
    {
        questDialouge.Clear();                                          //Cleares the Dialouge Que

        nameText.text = quest.questgiverName;                           //Sets the Name of the Quest giver

        foreach(string questSentences in quest.questDialouge)           //Display of the Dialouge
        {
            questDialouge.Enqueue(questSentences);
        }

        DisplayNextSentence();                                          //Displayes Next Sentence
    }

    public void DisplayNextSentence()
    {
        if(questDialouge.Count == 0)                                    //Simple If statment checking if there is more dialouge left
        {
            EndDialouge();
            return;
        }

        string dialouge = questDialouge.Dequeue();                      //Deques the current dialouge
        questText.text = dialouge;                                      //Sets the new dialouge
    }

    public void EndDialouge()
    {
        Debug.Log("End of quest convo");
        FindObjectOfType<QuestGiver>().FinishDialouge();           //initiates the End of the Players Dialouge
        
    }

    public void EndQuest()
    {
        if(levelBoss == null)                               //Checks that the Level boss still exists in the level
        {
            FindObjectOfType<QuestGiver>().FinishQuest();
        }
    }
}
