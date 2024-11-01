using System;
using UnityEngine;
using System.Collections;

public class EncounterHandler : MonoBehaviour
{
	public event Action<EncounterHandler, EnemyBase> OnEncounter;

	public void HandleEncounter(EnemyBase baseEnemy)
	{
        StartCoroutine(HandleEncounterCoroutine(baseEnemy));
	}

    private IEnumerator HandleEncounterCoroutine(EnemyBase baseEnemy)
    {
        BattleTransitionAnim.Instance.EnterCombat();
        yield return new WaitForSeconds(1.5f);
        OnEncounter?.Invoke(this, baseEnemy);
    }
}
