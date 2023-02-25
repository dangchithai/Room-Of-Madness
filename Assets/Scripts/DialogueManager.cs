using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI TMP_sentence;

    private Queue<string> sentences = new Queue<string>();
    private Player playerControl;
    private bool readyForNextSentence;
    private bool keyPressed;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        playerControl = FindObjectOfType<Player>().GetComponent<Player>();
        DontDestroyOnLoad(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (Input.anyKey && readyForNextSentence)
        {
            readyForNextSentence = false;
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        playerControl.enabled = false;
        sentences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string nextSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(nextSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        TMP_sentence.text = string.Empty;
        foreach (var letter in sentence.ToCharArray())
        {
            TMP_sentence.text += letter;
            yield return null;
        }
        readyForNextSentence = true;
    }

    private void EndDialogue()
    {
        playerControl.enabled = true;
    }
}
