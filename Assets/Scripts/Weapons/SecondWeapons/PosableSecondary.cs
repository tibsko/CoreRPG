using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosableSecondary : Secondary {
    [SerializeField] GameObject bullet;

    private GameObject sphere;
    private Vector3 endPosition;
    private Vector3 currentAim = Vector3.zero;
    private bool aiming;
    private Transform player;
    // Start is called before the first frame update
    void Start() {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(.4f, .4f,.4f);
        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = true;
        player = PlayerManager.instance.GetNearestPlayer(transform.position).transform;
    }

    void Update() {
        sphere.SetActive(aiming);
        if (aiming)
            ShowObject(currentAim);
    }
    public override void Attack() {
        base.Attack();
        Instantiate(bullet, endPosition, Quaternion.identity);
    }

    private void ShowObject(Vector3 aim) {
        endPosition = new Vector3(aim.x * range +transform.position.x, player.position.y, aim.y * range+transform.position.z);
        sphere.transform.position = endPosition;

    }
    public override void OnAim(Vector2 aim) {
        base.OnAim(aim);
        aiming = true;
        currentAim = aim;
    }
    public override void OnRelease(Vector2 aim) {
        aiming = false;
        Attack();
    }

}
