using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeWeapon : Weapon {

    [SerializeField] HitBox[] hitBoxes;
    public float knockBack = 5;

    private Animator animator;
    private bool comboAsked;

    protected void Start() {
        animator = GetComponentInParent<Animator>();
        foreach (HitBox hitBox in hitBoxes) {
            hitBox.onHit += Hit;
        }
    }

    public override void Attack() {
        //If already in attack, ask for combo.
        comboAsked = IsAttacking;

        IsAttacking = true;
        animator.SetBool("MeleeAttack", true);
    }

    public override bool CanAttack() {
        return true;
    }

    public void EndAttack(bool forceEnd = false) {
        //En attack if no combo is asked or if attack end is forced
        if (!comboAsked || forceEnd) {
            IsAttacking = false;
            onEndAttack.Invoke();
        }

        //When leaving attack, 
        comboAsked = false;
    }

    public void ToggleHitBoxes(bool state) {
        foreach (HitBox hitBox in hitBoxes) {
            hitBox.gameObject.SetActive(state);
        }
    }

    private void Hit(EnemyHealth enemyHealth) {
        enemyHealth.TakeDamage(damages, gameObject);
        Rigidbody rb = enemyHealth.GetComponent<Rigidbody>();
        if (rb) {
            ApplyKnockBack(rb);
        }
    }

    private void ApplyKnockBack(Rigidbody rigidbody) {
        //Vector3 knockBackVect = rigidbody.transform.position - animator.transform.position;
        //var control = rigidbody.GetComponent<SpitterController>();
        //StartCoroutine(ToggleControl(false, 0, rigidbody, control));
        //rigidbody.AddForce(knockBackVect.normalized * knockBack,ForceMode.VelocityChange);
        //StartCoroutine(ToggleControl(true, 1, rigidbody, control));

        //Debug2.Log("Knock back : " + knockBackVect);
    }

    private IEnumerator ToggleControl(bool status, float delay, Rigidbody rigidbody, MonoBehaviour script) {
        yield return new WaitForSeconds(delay);
        rigidbody.GetComponent<NavMeshAgent>().enabled = status;
        rigidbody.isKinematic = status;
        script.enabled = status;
    }

}
