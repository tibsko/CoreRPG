using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DevUtils {
    public static bool ContainsLayer(this LayerMask mask, int layer) {
        return mask == (mask | (1 << layer));
    }

    public static Collider GetClosest(this Vector3 position, ICollection<Collider> collection) {
        float distance = float.MaxValue;
        Collider closest = null;
        foreach (Collider go in collection) {
            float newDist = Vector3.Distance(position, go.transform.position);
            if (newDist < distance) {
                closest = go;
                distance = newDist; 
            }
        }

        return closest;
    }
}
