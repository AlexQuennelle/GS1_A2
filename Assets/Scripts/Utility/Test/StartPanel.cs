using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : PanelBase
{
    private Button button;
    protected override void Init()
    {
        button = GetController<Button>("startButton");
        button.onClick.AddListener(StartButtonClick);
    }

    private void StartButtonClick()
    {
        UIManager.Instance.HidePanel<StartPanel>(PanelBase.Ani.Fade);
        DialogueManager.Instance.BeginDialogue(1);
    }
}
