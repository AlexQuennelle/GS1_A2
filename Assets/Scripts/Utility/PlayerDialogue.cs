using UnityEngine;


[CreateAssetMenu(fileName = "PlayerDialogueDatabase", menuName = "Dialogue/PlayerDialogueDatabase")]
public class PlayerDialogueDatabase : ScriptableObject
{
    public string[] keys;
    public string[] dialogues;
    public string GetDialogue(string key)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i] == key)
            {
                return dialogues[i];
            }
        }

        Debug.LogWarning($"Dialogue with key '{key}' not found.");
        return null;
    }
}