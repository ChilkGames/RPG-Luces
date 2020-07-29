using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolObject
{
    void InitCallback();
    void DisposeCallback();
}
