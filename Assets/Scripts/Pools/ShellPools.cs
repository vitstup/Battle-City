using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShellPools : MonoBehaviour 
{
    private Dictionary<string, MonoPool<Shell>> pools = new Dictionary<string, MonoPool<Shell>>();

    [Inject] private DiContainer container;

    private void AddShellPool(Shell shell)
    {
        MonoPool<Shell> pool = new MonoPool<Shell>(shell, 5, container);
        pools.Add(shell.gameObject.name, pool);
    }

    public Shell GetShell(Shell shell)
    {
        string name = shell.gameObject.name;
        Debug.Log(name);
        if (!pools.ContainsKey(name)) AddShellPool(shell);
        return pools[name].GetElement();
    }

    public void ClearAllShells()
    {
        foreach(var pool in pools.Values)
        {
            var elements = pool.GetBusyElements();
            foreach(var element in elements)
            {
                element.gameObject.SetActive(false);
            }
        }
    }
}
