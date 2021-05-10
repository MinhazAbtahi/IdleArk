using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EZCameraShake;

[RequireComponent(typeof(Rigidbody))]
public class Missile : MonoBehaviour
{
    [Header("Setup")]
    public Transform target;
    private Rigidbody rigidBody;
    private Transform localTransform;
    public float turnSpeed = 1f;
    public float rocketFlySpeed = 10f;
    private WaitForSeconds lifeTime = new WaitForSeconds(2f);
    public GameObject explosionFX;

    // Start is called before the first frame update
    void Start()
    {
        localTransform = GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody>();

        if (target)
        {
            StartCoroutine(ExplodeRoutine());
        }
    }


    private void FixedUpdate()
    {
        if (!target) 
            return;

        rigidBody.velocity = localTransform.forward * rocketFlySpeed;

        //Now Turn the Rocket towards the Target
        var rocketTargetRot = Quaternion.LookRotation(target.position - localTransform.position);

        rigidBody.MoveRotation(Quaternion.RotateTowards(localTransform.rotation, rocketTargetRot, turnSpeed));
    }

    private void Explode()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        //Camera.main.transform.DOShakeRotation(0.45f, 1, 10, 90);
        //CameraShaker.Instance.ShakeOnce(.5f, .5f, .05f, .1f);
        //Deactivate Rocket..
        this.gameObject.SetActive(false);
    }

    private IEnumerator ExplodeRoutine()
    {
        yield return lifeTime;

        Explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody plRgb = other.gameObject.GetComponent<Rigidbody>();
            if (plRgb)
            {
                plRgb.AddForceAtPosition(Vector3.up * 1000f, plRgb.position);
            }
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    Rigidbody plRgb = collision.gameObject.GetComponent<Rigidbody>();
        //    if (plRgb)
        //    {
        //        plRgb.AddForceAtPosition(Vector3.up * 1000f, plRgb.position);
        //    }
        //    Explode();
        //}
    }

}
