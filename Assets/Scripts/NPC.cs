using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : Talker
{
    private PlayerController player;

    private SpriteRenderer npcSprite;
    
    private DialogueTrigger dialogue;

    private bool dialoguePaused;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePaused = false;
        player = FindObjectOfType<PlayerController>();
        npcSprite = GetComponentInChildren<SpriteRenderer>();
        dialogue = GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    private void Flip()
    {
        if (player.transform.position.x > transform.position.x)
            npcSprite.flipX = false;
        else npcSprite.flipX = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!dialogue.dialogue.isDoneOnce || !dialoguePaused)
                dialogue.TriggetDialogue();
            if (dialoguePaused)
            {
                dialoguePaused = false;
                dialogue.ResumeDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            dialoguePaused = true;
            dialogue.PauseDialogue();
        }
    }

    public void StartDialogueButton()
    {
        if (dialogue.dialogue.isDoneOnce)
            dialogue.TriggetDialogue();
    }
}
