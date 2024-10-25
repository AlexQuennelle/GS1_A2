using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DialogueManager.Instance.BeginDialogue(6);
        gameObject.SetActive(false);
    }
}
