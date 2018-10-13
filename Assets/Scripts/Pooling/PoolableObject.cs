using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour {
    internal ObjectPooling parentPool;

    public void Free()
    {
        parentPool.Free(gameObject);
    }
}
