using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour {
    public float movementSpeed;

    [SerializeField] Transform playerModel;

    public Transform targetTransform;

    public bool isAlive = true;

    public Animator animator;

    // Start is called before the first frame update
    protected virtual void Start() {
        animator.Play("Idle");
    }

    protected virtual void FixedUpdate() {
        if (targetTransform == null)
            return;

        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        Move();

        if (Mathf.Abs(targetTransform.position.x - transform.position.x) < 0.05f && Mathf.Abs(targetTransform.position.z - transform.position.z) < 0.05f) {
            animator.Play("Idle");
            targetTransform = null;
        }
    }

    private void Move() {
        animator.Play("Run");

        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position + new Vector3(-0f, 1, -0f), movementSpeed * Time.fixedDeltaTime);
    }

    public void SetTransform(Transform t) {
        targetTransform = t;

        Rotate(targetTransform.position - transform.position);
    }


    void Rotate(Vector3 inputDirection) {
        float angle = 90 + Mathf.Rad2Deg * Mathf.Atan2(-inputDirection.z, inputDirection.x);
        transform.localEulerAngles = Vector3.up * angle;

        playerModel.localEulerAngles = Vector3.zero;
        playerModel.localPosition = Vector3.zero;
    }


}
