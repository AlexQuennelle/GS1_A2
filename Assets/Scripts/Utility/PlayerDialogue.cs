using UnityEngine;


[CreateAssetMenu(fileName = "PlayerDialogueDatabase", menuName = "Dialogue/PlayerDialogueDatabase")]
public class PlayerDialogueDatabase : ScriptableObject
{
    public string[] keys;
    public string[] dialogues;
}