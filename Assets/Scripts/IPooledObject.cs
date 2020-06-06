using System.Numerics;
using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn(UnityEngine.Vector2 direction); 
}
