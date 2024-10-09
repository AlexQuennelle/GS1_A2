using System.Collections.Generic;
using UnityEngine;
using Utility.Singleton;

public class DialogueManager : NoMonoSingleton<DialogueManager>
{
    private Dictionary<string, string> dialogueDictionary = new Dictionary<string, string>();

    public void Initialize()
    {
        PlayerDialogueDatabase dialogueDatabase = Resources.Load<PlayerDialogueDatabase>("DialogueData/Dialogue");

        if (dialogueDatabase != null)
        {
            for (int i = 0; i < dialogueDatabase.keys.Length; i++)
            {
                if (i < dialogueDatabase.dialogues.Length)
                {
                    string key = dialogueDatabase.keys[i];
                    string dialogueText = dialogueDatabase.dialogues[i];

                    if (!string.IsNullOrEmpty(key))
                    {
                        dialogueDictionary[key] = dialogueText;
                    }
                }
            }

            Debug.Log("Dialogue data successfully initialized and stored in the dictionary.");
        }
        else
        {
            Debug.LogWarning("PlayerDialogueDatabase not found in Resources folder!");
        }
    }

    public void GetDialogue(string key)
    {
        if (dialogueDictionary.TryGetValue(key, out string dialogueText))
        {
            UIManager.Instance.ShowPanel<DialoguePanel>(PanelBase.PanelShowLayer.Front, PanelBase.Ani.Fade, dialoguePanel =>
            {
                dialoguePanel.SetText(dialogueText);
            });
        }
        else
        {
            Debug.LogWarning("Dialogue with key '" + key + "' not found!");
        }
    }
}