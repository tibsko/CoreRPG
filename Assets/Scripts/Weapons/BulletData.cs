using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class BulletData : ScriptableObject
{
    public float maxDistance;
    public float radius;
    public float explosionForce;
    public float lifeTime;
    public int damage;

}
