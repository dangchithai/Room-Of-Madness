using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue DialogueContent;
    public bool TriggerOnStart;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(DialogueContent);
    }

    private void Start()
    {
        if (TriggerOnStart)
        {
            TriggerDialogue();
        }
    }
}
