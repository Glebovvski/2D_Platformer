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

    private bool paused;

    [SerializeField]
    private AudioSource typeSound;

    private void Start()
    {
        paused = false;
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
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        if (!paused)
        {
            DialogueElements currentDialogue = sentences.Dequeue();
            string sentence = currentDialogue.sentence;
            if (currentTalker != null)
                currentTalker.bubbleCanvas.SetActive(false);
            currentTalker = currentDialogue.Talker;
            currentTalker.bubbleCanvas.SetActive(true);
            StartCoroutine(TypeSentence(sentence));
        }
        if (paused)
            currentTalker.bubbleCanvas.SetActive(false);
    }

    IEnumerator TypeSentence(string sentence)
    {
        currentTalker.bubbleText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            currentTalker.bubbleText.text += letter;
            typeSound.Play();
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2);
        DisplayNextSentence();
    }

    private void EndDialogue()
    {
        currentTalker.bubbleCanvas.SetActive(false);
    }

    public void PauseDialogue()
    {
        paused = true;
    }

    public void ResumeDialogue()
    {
        paused = false;
        currentTalker.bubbleCanvas.SetActive(true);
        DisplayNextSentence();
    }
}
