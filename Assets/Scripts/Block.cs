using UnityEngine;

public class Block : MonoBehaviour, IDieble, IRevivable
{
    public void TakeDamage(float damage)
    {
        Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Revive()
    {
        gameObject.SetActive(true);
    }
}