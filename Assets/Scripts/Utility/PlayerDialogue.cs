using UnityEngine;

[System.Serializable]
public class PlayerDialogueEntry
{
    public string key;
    [TextArea(2, 5)]
    public string dialogueText;
}

[CreateAssetMenu(fileName = "PlayerDialogueDatabase", menuName = "Dialogue/PlayerDialogueDatabase")]
public class PlayerDialogueDatabase : ScriptableObject
{
    public PlayerDialogueEntry[] dialogues;
}