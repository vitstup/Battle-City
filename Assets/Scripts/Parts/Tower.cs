using UnityEngine;
using Zenject;

public class Tower : MonoBehaviour, IShootable
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector2 shootPoint;

    [SerializeField] private float reloadTime;
    [SerializeField] protected Shell shell;

    [SerializeField] private Collider2D selfCollider;

    private Vector2 lookPosition;
    private float reloadTimer;

    [Inject] private ShellPools shellPools;
    
    private void Update()
    {
        #region rotation
        Vector2 direction = (lookPosition - (Vector2)transform.position).normalized;
        float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetRotation -= 90f;

        Quaternion targetQuaternion = Quaternion.Euler(0f, 0f, targetRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, rotationSpeed * Time.deltaTime);
        #endregion

        #region reload
        if (reloadTimer >= 0) reloadTimer -= Time.deltaTime;
        #endregion
    }

    public void LookTo(Vector2 position)
    {
        lookPosition = position;
    }

    public void Shoot()
    {
        if (reloadTimer <= 0)
        {
            var shellRound = shellPools.GetShell(shell);
            shellRound.transform.position = transform.TransformPoint(shootPoint);
            shellRound.Shoot(transform.rotation, selfCollider);
            reloadTimer = reloadTime;
        }
    }
}
