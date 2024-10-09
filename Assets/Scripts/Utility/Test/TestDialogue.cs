using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DialogueManager.Instance.GetDialogue("Alpha1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DialogueManager.Instance.GetDialogue("Alpha2");
        }
    }
}
