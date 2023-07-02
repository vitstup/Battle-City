using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonoPool<T> where T : MonoBehaviour
{
    private List<T> pool = new List<T>();
    private T prefab;
    private DiContainer container;

    public MonoPool(T prefab, int baseSize, DiContainer container)
    {
        this.container = container;
        this.prefab = prefab;
        CreatePool(baseSize);
    }

    private void CreatePool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateElement();
        }
    }

    private T CreateElement(bool isActiveByDefault = false)
    {
        var element = container.InstantiatePrefab(prefab).GetComponent<T>();
        element.gameObject.SetActive(isActiveByDefault);
        pool.Add(element);
        return element;
    }

    private bool HaveFreeElement(out T element)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                element = pool[i];
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetElement()
    {
        T result = null;
        if (HaveFreeElement(out var element))
        {
            result = element;
            result.gameObject.SetActive(true);
        }
        else result = CreateElement(true);
        return result;
    }

    public T[] GetBusyElements()
    {
        List<T> busy = new List<T>();
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].gameObject.activeSelf) busy.Add(pool[i]);
        }
        return busy.ToArray();
    }
}