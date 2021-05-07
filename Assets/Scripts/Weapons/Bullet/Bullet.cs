using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {

    public float Range { get; protected set; }
    public float Damages { get; protected set; }

    protected bool initialized = false;

    public abstract void InitializeBullet(Vector3 rotation, float _damages, float _velocity, float _range);

}