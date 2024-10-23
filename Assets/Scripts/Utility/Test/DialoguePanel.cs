using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : PanelBase
{
    private Text textDialogue;
    private Button button_Close;
    private DataStruct data;
    private float typingSpeed;

    protected override void Init()
    {
        typingSpeed = 0.1f;
        textDialogue = GetController<Text>("Text_Dialogue");
        button_Close = GetController<Button>("Button_Close");
        button_Close.onClick.AddListener(CloseButtonClick);
        SetText(data.dialogue);
    }

    public void SetData(DataStruct dataStruct)
    {
        data = dataStruct;
    }

    public void SetText(string dialogueText)
    {
        if (textDialogue != null)
        {
            StartCoroutine(TypeDialogue(dialogueText));
        }
        else
        {
            Debug.LogWarning("Text component is not assigned or found in DialoguePanel.");
        }
    }

    private void CloseButtonClick()
    {
        if (data.nextId == 0)
        {
            UIManager.Instance.HidePanel<DialoguePanel>(PanelBase.Ani.Fade);
        } 
        else
        {
            data = DialogueManager.Instance.GetDialogue(data.nextId);
            SetText(data.dialogue);
        }
    }

    IEnumerator TypeDialogue(string fullText)
    {
        textDialogue.text = "";
        foreach (char letter in fullText.ToCharArray())
        {
            textDialogue.text += letter;  // 将字母逐个添加到对话框
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}