using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingDoor : MonoBehaviour
{
    [HideInInspector]
    public static bool isBossDefeated = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Open", false);
        animator.SetBool("Close", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            if (collision.transform.position.x < this.transform.position.x || isBossDefeated)
            {
                animator.SetBool("Open", true);
                animator.SetBool("Close", false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("Open", false);
        animator.SetBool("Close", true);
    }

}
