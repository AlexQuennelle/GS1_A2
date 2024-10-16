using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : PanelBase
{
    private Text text;
    private Button button_Close;

    protected override void Init()
    {
        button_Close = GetController<Button>("Button_Close");
        button_Close.onClick.AddListener(CloseButtonClick);
    }

    public void SetText(string dialogueText)
    {
        text = GetController<Text>("Text_Dialogue");
        if (text != null)
        {
            text.text = dialogueText;
        }
        else
        {
            Debug.LogWarning("Text component is not assigned or found in DialoguePanel.");
        }
    }

    private void CloseButtonClick()
    {
        UIManager.Instance.HidePanel<DialoguePanel>(PanelBase.Ani.Fade);
    }

}