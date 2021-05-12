using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosableSecondary : Secondary {
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject previs;
    [SerializeField] Material mat;

    private GameObject posableRenderer;
    private Vector3 endPosition;
    private Vector3 currentAim = Vector3.zero;
    private bool aiming;
    private Transform player;
    // Start is called before the first frame update
    void Start() {
        posableRenderer = Instantiate(previs, transform.position, Quaternion.identity);
        player = PlayerManager.instance.GetNearestPlayer(transform.position).transform;

       
    }

    void Update() {
        posableRenderer.SetActive(aiming);
        if (aiming)
            ShowObject(currentAim);
    }
    public override void Attack() {
        base.Attack();
        Instantiate(bullet, endPosition, Quaternion.identity);
    }

    private void ShowObject(Vector3 aim) {
        endPosition = new Vector3(aim.x * range, 0, aim.y * range)+player.position;
        posableRenderer.transform.position = endPosition+Vector3.up*.2f;

    }
    public override void OnAim(Vector2 aim) {
        base.OnAim(aim);
        aiming = true;
        currentAim = aim;
    }
    public override void OnRelease(Vector2 aim) {
        aiming = false;
        if(posableRenderer.GetComponent<PosablePrevisualisation>().CanPos)
            Attack();
        else {
            Debug.Log("Can't Pos");
        }
    }

}
