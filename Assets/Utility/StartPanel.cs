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
        Debug.Log("Welcome");
        button = GetController<Button>("startButton");
        button.onClick.AddListener(startButtonClick);
    }

    private void startButtonClick()
    {
        Debug.Log("Start Game");
    }
}
