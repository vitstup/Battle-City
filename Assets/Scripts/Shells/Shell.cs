using UnityEngine;
using Zenject;

public class Shell : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float effectSize;

    [Inject] private ExplosionsManager exploisonManager;

    private Collider2D ignoreCollider;

    public void Shoot(Quaternion angle, Collider2D collider)
    {
        SetDirection(angle);
        SetIgnore(collider);
    }

    private void SetDirection(Quaternion angle)
    {
        transform.rotation = angle;
    }

    private void SetIgnore(Collider2D collider)
    {
        ignoreCollider = collider;
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Booster>() != null) return;
        if (collision != null && collision != ignoreCollider)
        {
            Hitted(collision);

            exploisonManager = FindObjectOfType<ExplosionsManager>();
            exploisonManager.ShowExplosion(transform.position, effectSize);

            gameObject.SetActive(false);
        }
    }

    protected virtual void Hitted(Collider2D collision)
    {
        var damagable = collision.GetComponent<IDamagable>();
        if (damagable != null) 
        {
            damagable.TakeDamage(damage);
        }
    }
}