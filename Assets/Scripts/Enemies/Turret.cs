using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] private Tower tower;

    private void Update()
    {
        Collider2D[] colliders = GetSphereOfView();

        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
                if (IsObjectVisible(collider)) 
                {
                    tower.LookTo(collider.transform.position);
                    tower.Shoot();
                }
            }
        }
    }

}