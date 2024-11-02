using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Singleton;

public class BattleTransitionAnim : MonoSingleton<BattleTransitionAnim>
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void EnterCombat()
    {
        int triggerId = Animator.StringToHash("battleEnter");
        anim.SetTrigger(triggerId);
    }

    public void ExitCombat()
    {
        int triggerId = Animator.StringToHash("battleExit");
        anim.SetTrigger(triggerId);
    }
}
