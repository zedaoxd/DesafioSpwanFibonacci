using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    void OnInstantiated();
    void OnEnabledFromPool();
    void OnDisabledFromPool();
}
