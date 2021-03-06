using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    #region Singleton
    public static LayerManager instance;
    void Awake() {
        if (LayerManager.instance != null) {
            Destroy(this);
        }
        else
            instance = this;
    }
    #endregion

    public LayerMask groundLayer;
    public LayerMask bulletLayer;
}
