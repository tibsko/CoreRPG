using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour {

    public List<string> activeDebug;
    public bool enableDebug = true;

    void Awake() {
        Debug2.activeDebugManager = this;
    }
}

public static class Debug2 {

    public static DebugManager activeDebugManager;
    public static void Log(object message, string chanel = "main") {
        //if (activeDebugManager) {
        //    if (activeDebugManager.activeDebug.Contains(chanel))
        //        Debug.Log(message);
        //}
        //else
        //    Debug.Log(message);
    }
}


//shotgun 
//float angle = shotGunData.shotAngle / NbBulletToShoot;
//for (int i = 0; i < NbBulletToShoot; i++) {
//    Vector3 bulletRotation = new Vector3(0, (i * angle) - shotGunData.shotAngle / 2, 0) + firePoint.rotation.eulerAngles;
//    bulletRotation = new Vector3(0, bulletRotation.y, 0);
//    GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(bulletRotation));
//}