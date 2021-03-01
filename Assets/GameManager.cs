using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region
    public static GameManager instance;
    void Awake()
    {
        if(GameManager.instance!=null)
        {
            Destroy(this);
        }
        else
            instance = this;
    }
    #endregion

    public LayerMask groundLayer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
