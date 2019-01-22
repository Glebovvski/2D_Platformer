using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : Talker
{
    private PlayerController player;

    private SpriteRenderer npcSprite;
    
    private DialogueTrigger dialogue;

    // Start is called before the first frame update
    void Start()
    {
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
            if (!dialogue.dialogue.isDoneOnce)
                dialogue.TriggetDialogue();
        }
    }

    public void StartDialogueButton()
    {
        if (dialogue.dialogue.isDoneOnce)
            dialogue.TriggetDialogue();
    }
}
