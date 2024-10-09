using System.Collections.Generic;
using UnityEngine;
using Utility.Singleton;

public class DialogueManager : NoMonoSingleton<DialogueManager>
{
    private Dictionary<string, string> dialogueDictionary = new Dictionary<string, string>();

    public void Initialize()
    {
        PlayerDialogueDatabase dialogueDatabase = Resources.Load<PlayerDialogueDatabase>("DialogueData/PlayerDialogueDatabase");
        if (dialogueDatabase != null)
        {
            foreach (var entry in dialogueDatabase.dialogues)
            {
                dialogueDictionary[entry.key] = entry.dialogueText;
            }
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