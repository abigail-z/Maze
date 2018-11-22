using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get { return instance; } }
    private static ObjectPooler instance;

    public PoolType[] poolTypes;
    private Dictionary<string, ObjectPool> dict;

	void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dict = new Dictionary<string, ObjectPool>();
        foreach (PoolType type in poolTypes)
        {
            ObjectPool pool = new ObjectPool
            {
                prefab = type.prefab,
                stack = new Stack<Poolable>()
            };
            dict.Add(type.name, pool);
        }

        poolTypes = null;
	}
	
	public void Push (string poolName, Poolable obj)
    {
        ObjectPool pool;
        if (dict.TryGetValue(poolName, out pool))
        {
            obj.gameObject.SetActive(false);
            pool.stack.Push(obj);
        }
    }

    public Poolable Pop (string poolName)
    {
        ObjectPool pool;
        if (dict.TryGetValue(poolName, out pool))
        {
            Poolable obj;
            if (pool.stack.Count > 0)
            {
                obj = pool.stack.Pop();
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                obj = Instantiate(pool.prefab);
                obj.pool = this;
                obj.transform.parent = transform;
                return obj;
            }
        }

        return null;
    }
}

public class Poolable : MonoBehaviour
{
    [HideInInspector]
    public ObjectPooler pool;
}

[Serializable]
public struct PoolType
{
    public string name;
    public Poolable prefab;
}

public struct ObjectPool
{
    public Poolable prefab;
    public Stack<Poolable> stack;
}
