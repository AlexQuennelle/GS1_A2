using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Utility.Singleton;

public struct DataStruct
{
    public int id;
    public int nextId;
    public string dialogue;
}

public class DialogueManager : NoMonoSingleton<DialogueManager>
{
    private Dictionary<int, DataStruct> dialogueDictionary = new Dictionary<int, DataStruct>();

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
                        string[] parts = key.Split(',');
                        int num1 = int.Parse(parts[0]);
                        int num2 = int.Parse(parts[1]);
                        DataStruct data = new DataStruct
                        {
                            id = num1,
                            nextId = num2,
                            dialogue = dialogueText
                        };
                        dialogueDictionary[num1] = data;
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

    public void BeginDialogue(int id)
    {
        if (dialogueDictionary.TryGetValue(id, out DataStruct data))
        {
            UIManager.Instance.ShowPanel<DialoguePanel>(PanelBase.PanelShowLayer.Forefront, PanelBase.Ani.None, dialoguePanel =>
            {
                dialoguePanel.SetData(data);
            });
        }
        else
        {
            Debug.LogWarning("Dialogue with key '" + id + "' not found!");
        }
    }

    public DataStruct GetDialogue(int id)
    {
        if (dialogueDictionary.TryGetValue(id, out DataStruct data))
        {
            return data;
        }
        else
        {
            throw new KeyNotFoundException($"ID {id} not found in the dialogue dictionary.");
        }
    }
}