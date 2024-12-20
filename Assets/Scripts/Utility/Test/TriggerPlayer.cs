using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayer : MonoBehaviour
{
    [SerializeField]
    private int dialogueStart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DialogueManager.Instance.BeginDialogue(dialogueStart);
            gameObject.SetActive(false);
        }
    }
}
