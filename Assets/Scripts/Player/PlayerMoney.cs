using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public int currentMoney;
    // Start is called before the first frame update
    void Start()
    {
        HUD.instance.UpdateMoney(currentMoney);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddMoney(int money) {
        currentMoney += money;
        HUD.instance.UpdateMoney(currentMoney);
    }
    public void LooseMoney(int money) {
        currentMoney -= money;
        HUD.instance.UpdateMoney(currentMoney);
    }
}
