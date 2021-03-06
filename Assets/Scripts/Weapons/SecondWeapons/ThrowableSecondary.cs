﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableSecondary : Secondary {
    [SerializeField] GameObject bullet;
    [SerializeField] LineRenderer lr;

    private ParabolicProjectile parabolicProjectile;
    private Vector3 endPosition;
    private Vector3 currentAim = Vector3.zero;
    private bool aiming;
    private Transform player;

    // Start is called before the first frame update
    void Start() {
        //lr = GetComponent<LineRenderer>();
        player = ReferenceManager.instance.GetNearestPlayer(transform.position).transform;
    }

    void Update() {
        lr.enabled = aiming;
        if (aiming)
            ShowParabole(currentAim);
    }
    public override void Attack() {
        base.Attack();
        Instantiate(bullet, player.position, Quaternion.identity);
    }

    private void ShowParabole(Vector3 aim) {
        parabolicProjectile = bullet.GetComponent<ParabolicProjectile>();
        endPosition = new Vector3(aim.x * range, bullet.transform.position.y, aim.y * range) + player.position;
        parabolicProjectile.SetTarget(endPosition);
        int count = 20;
        Vector3[] arcArray = new Vector3[count + 1];
        for (int i = 0; i <= count; i++) {
            arcArray[i] = ParabolaEquation.Parabole(player.position, endPosition, parabolicProjectile.height, i / (float)count);
        }
        lr.positionCount = count + 1;
        lr.SetPositions(arcArray);
    }

    public override void OnAim(Vector2 aim) {
        base.OnAim(aim);
        aiming = true;
        currentAim = aim;
    }

    public override void OnRelease(Vector2 aim) {
        aiming = false;
        GameObject bulletGo = Instantiate(bullet, transform.position, Quaternion.identity);
        parabolicProjectile = bulletGo.GetComponent<ParabolicProjectile>();
        endPosition = new Vector3(currentAim.x * range, bullet.transform.position.y, currentAim.y * range) + player.position;
        parabolicProjectile.SetTarget(endPosition);
        parabolicProjectile.throwObject = true;


    }
}
