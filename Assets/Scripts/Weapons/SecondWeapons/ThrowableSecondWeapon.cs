using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableSecondWeapon : SecondWeapon {
    [SerializeField] GameObject bullet;

    private ParabolicProjectile parabolicProjectile;
    private Vector3 endPosition;
    [SerializeField] LineRenderer lr;

    // Start is called before the first frame update
    void Start() {
        //lr = GetComponent<LineRenderer>();
    }
    public override void Attack() {
        base.Attack();
        Transform player = PlayerManager.instance.GetNearestPlayer(transform.position).transform;
        Instantiate(bullet, player.position, Quaternion.identity);
    }

    public override void OnAim(Vector2 aim) {
        base.OnAim(aim);
        Debug.Log("ren");
        parabolicProjectile = bullet.GetComponent<ParabolicProjectile>();
        parabolicProjectile.SetTarget(new Vector3(aim.x*10, bullet.transform.position.y, aim.y*10)+transform.position);
        endPosition = new Vector3(aim.x, bullet.transform.position.y, aim.y)+transform.position;
        int count = 20;
        Vector3[] arcArray = new Vector3[count+1];
        for (int i = 0; i <= count; i++) {
            arcArray[i] = ParabolaEquation.Parabole(transform.position, endPosition, parabolicProjectile.height, i / (float)count );
        }
        lr.positionCount=count+1;
        lr.SetPositions(arcArray);
    }
}
