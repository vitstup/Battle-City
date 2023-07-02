using UnityEngine;
using Zenject;

public class InputController : MonoBehaviour
{
    [Inject] private Tank player;

    [Inject] private RestartManager restartManager;

    private void Update()
    {
        if (player.gameObject.activeSelf)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            player.tower.LookTo(mousePos);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) player.hull.MoveForward();
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) player.hull.MoveBackward();

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) player.hull.Rotate(1);
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) player.hull.Rotate(-1);

            if (Input.GetMouseButtonDown(0)) player.tower.Shoot();

            if (player.tower is MultiAmmoTower)
            {
                MultiAmmoTower tower = player.tower as MultiAmmoTower;
                for (int i = 0; i < tower.AmmoVariants(); i++)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1 + i)) tower.ChangeShell(i);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) restartManager.Restart();

    }
}