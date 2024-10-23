using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DialogueManager.Instance.BeginDialogue(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DialogueManager.Instance.BeginDialogue(2);
        }
    }
}
