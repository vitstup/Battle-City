using UnityEngine;

public class HealthBooster : Booster
{
    [SerializeField] private float healthBonus;

    protected override void Entered(Collider2D collider)
    {
        IHealable healable = collider.GetComponent<IHealable>();

        if (healable != null)
        {
            healable.Heal(healthBonus);
            gameObject.SetActive(false);
        }

    }
}