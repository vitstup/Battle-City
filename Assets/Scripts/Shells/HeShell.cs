using UnityEngine;

public class HeShell : Shell
{
    [SerializeField] private float splashRadius;

    protected override void Hitted(Collider2D collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, splashRadius);

        foreach (Collider2D collider in colliders)
        {
            var damagable = collider.GetComponent<IDamagable>();
            if (damagable != null) damagable.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, splashRadius);
    }
}