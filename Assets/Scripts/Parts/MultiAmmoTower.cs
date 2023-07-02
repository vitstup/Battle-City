using UnityEngine;

public class MultiAmmoTower : Tower
{
    [SerializeField] private Shell[] shells;

    public void ChangeShell(int id)
    {
        shell = shells[id];
    }

    public void ChangeShell(Shell shell)
    {
        this.shell = shell;
    }

    public int AmmoVariants()
    {
        return shells.Length;
    }
}