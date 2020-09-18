using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [Header("Quest Giver Name")]
    public string questgiverName;               //Name of the Quest givver
    [Space]

    [TextArea(3, 8)]
    public string[] questDialouge;              //Dialouge box to type in the Quest
}
