using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CombatPanel : PanelBase
{
    private Image hpPlayer;
    private Image hpEnemy;
    private Health playerHealth;
    private Health enemyHealth;
    private int maxEnemyHealth;

    protected override void Init()
    {
        hpPlayer = GetController<Image>("hpPlayer");
        hpEnemy = GetController<Image>("hpEnemy");
        maxEnemyHealth = enemyHealth.HP;
        hpEnemy.rectTransform.localScale = new Vector2(1, 1);
        StartCoroutine(ExecuteEveryFrame());
    }

    public void GetHealthData(Health player, Health enemy)
    {
        playerHealth = player;
        enemyHealth = enemy;
    }

    IEnumerator ExecuteEveryFrame()
    {
        while (true)
        {
            hpPlayer.rectTransform.localScale = new Vector2((float)playerHealth.HP / 15, 1);
            hpEnemy.rectTransform.localScale = new Vector2((float)enemyHealth.HP / maxEnemyHealth, 1);
            yield return null;
        }
    }
}
