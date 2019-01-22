using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;

    public static DialogueManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Dialogue Manager instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private Queue<DialogueElements> sentences;
    private Talker currentTalker;

    private GameObject Speaker;

    private void Start()
    {
        sentences = new Queue<DialogueElements>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogue.isDoneOnce = true;
        sentences.Clear();

        foreach (DialogueElements sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Debug.Log(sentences.Count);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        DialogueElements currentDialogue = sentences.Dequeue();
        string sentence = currentDialogue.sentence;
        if (currentTalker != null)
            currentTalker.bubbleCanvas.SetActive(false);
        currentTalker = currentDialogue.Talker;
        currentTalker.bubbleCanvas.SetActive(true);
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        currentTalker.bubbleText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            currentTalker.bubbleText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2);
        DisplayNextSentence();
    }

    private void EndDialogue()
    {
        currentTalker.bubbleCanvas.SetActive(false);
        
    }
}
