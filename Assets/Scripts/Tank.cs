using UnityEngine;
using System.Collections;
using Zenject;

public class Tank : MonoBehaviour, IDieble, IRevivable, IHealable, IInvulnerable
{
    [field: SerializeField] public Hull hull { get; private set; }
    [field: SerializeField] public Tower tower { get; private set; }

    [SerializeField] private float health;

    private float currentHealth;

    private Vector2 startPos;

    private bool isInInvul;

    [Inject] private HealthBar healthBar;
    [Inject] private LoseManager loseManager;

    private SpriteRenderer[] renderers;

    private void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();

        currentHealth = health;
        startPos = transform.position;
        ChangeHealthUI();
    }

    public void TakeDamage(float damage)
    {
        if (!isInInvul)
        {
            currentHealth -= damage;
            if (currentHealth <= 0) Die();
            ChangeHealthUI();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        loseManager.Lose();
    }

    public void Revive()
    {
        gameObject.SetActive(true);

        currentHealth = health;

        transform.position = startPos;

        transform.rotation = Quaternion.identity;

        ChangeHealthUI();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > health) currentHealth = health;
        ChangeHealthUI();
    }

    public void BecomeInvulnerable(float time)
    {
        if (renderers == null) renderers = GetComponentsInChildren<SpriteRenderer>();
        if (!isInInvul) StartCoroutine(InvulRoutine(time));
    }

    private IEnumerator InvulRoutine(float invulTime)
    {
        isInInvul = true;

        Color[] colors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            colors[i] = renderers[i].color;
            renderers[i].color = new Color(1, 1, 1, 0.5f);
        }

        yield return new WaitForSeconds(invulTime);

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = colors[i];
        }

        isInInvul = false;
    }

    private void ChangeHealthUI()
    {
        healthBar.SetBar(currentHealth / health);
    }
}