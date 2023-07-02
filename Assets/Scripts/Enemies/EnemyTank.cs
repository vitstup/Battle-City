using UnityEngine;

public class EnemyTank : Enemy
{
    [SerializeField] private Hull hull;
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

        RandomMove();
    }

    private float stopDuration = 1f; // ����������������� ���������
    private float rotationTimer = 0f; // ������ ��� ������������ ����������������� ��������
    private bool isStopped = false; // ����, ����������� �� ��, ���������� �� ���� � ������ ������

    private void RandomMove()
    {
        if (isStopped)
        {
            rotationTimer += Time.deltaTime;

            if (rotationTimer >= stopDuration)
            {
                isStopped = false;
                rotationTimer = 0f;
            }
        }
        else
        {
            hull.MoveForward();
        }

        RotateUntilClear();
    }

    private void RotateUntilClear()
    {
        hull.Rotate(1f); // ������������ �� 1 ������

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, hull.transform.up, 1f);

        bool haveObstacles = false;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == selfCollider) continue;
            else
            {
                haveObstacles = true;
                break;
            }
        }

        if (haveObstacles)
        {
            isStopped = true;
        }
    }

}