using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IDieble, IRevivable, IHealable, IInvulnerable
{
    public static UnityEvent enemyDied = new UnityEvent();

    [SerializeField] private float lookDistance;
    [SerializeField] private float health;

    private float currentHealth;

    protected Collider2D selfCollider;

    private Vector2 startPos;

    private bool isInInvul;

    private SpriteRenderer[] renderers;

    private void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();

        startPos = transform.position;
        selfCollider = GetComponent<Collider2D>();
        currentHealth = health;
    }

    protected Collider2D[] GetSphereOfView()
    {
        return Physics2D.OverlapCircleAll(transform.position, lookDistance);
    }

    protected bool IsObjectVisible(Collider2D collider)
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, collider.transform.position);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == selfCollider || hit.collider == collider) continue;
            else return false;
        }

        return true;
    }

    public void TakeDamage(float damage)
    {
        if (!isInInvul)
        {
            currentHealth -= damage;
            if (currentHealth <= 0) Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        enemyDied?.Invoke();
    }

    public void Revive()
    {
        gameObject.SetActive(true);

        currentHealth = health;

        transform.position = startPos;

        transform.rotation = Quaternion.identity;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > health) currentHealth = health;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookDistance);
    }

}