using System.Linq;
using UnityEngine;
using Zenject;

public class RestartManager : MonoBehaviour
{
    [Inject] private LoseUI loseUI;
    [Inject] private ShellPools shellPools;

    private IRevivable[] revilables;

    private Enemy[] enemies;

    private void Start()
    {
        Enemy.enemyDied.AddListener(EnemyDied);
    }

    private void FindEnemies()
    {
        enemies = FindObjectsOfType<Enemy>(true);
    }

    private void FindDiebles()
    {
        revilables = FindObjectsOfType<MonoBehaviour>(true).OfType<IRevivable>().ToArray();
    }

    public void Restart()
    {
        if (revilables == null || revilables.Length == 0) FindDiebles();

        foreach (var revilabe in revilables)
        {
            revilabe.Revive();
        }

        shellPools.ClearAllShells();

        Time.timeScale = 1.0f;

        loseUI.HideCanvas();
    }

    private void EnemyDied()
    {
        if (enemies == null) FindEnemies();

        bool anyoneIsLiving = false;

        foreach (Enemy enemy in enemies)
        {
            if (enemy.gameObject.activeSelf) { anyoneIsLiving = true; break; }
        }

        if (!anyoneIsLiving)
        {
            Restart();
        }
    }
}