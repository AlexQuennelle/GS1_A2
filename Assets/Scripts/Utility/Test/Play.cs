using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.ShowPanel<StartPanel>(PanelBase.PanelShowLayer.Front, PanelBase.Ani.Fade);
        DialogueManager.Instance.Initialize();
    }
}
