using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBulletController : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float speed;

    private Rigidbody rigidBody;
    private bool canMoving = false;
    private Vector3 direction;
    private Transform target;

    [SerializeField] bool isTarget = false;

    private void Start()
    {
        Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<IHealth>() != null)
        {
            collision.gameObject.GetComponent<IHealth>().OnDamage(damage, transform.position - collision.transform.position);
            DestroyThis();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IHealth>() != null)
        {
            other.gameObject.GetComponent<IHealth>().OnDamage(damage, transform.position - other.transform.position);
            DestroyThis();
        }
    }

    public void Moving()
    {
        if (!canMoving) return;
        //rigidBody.AddForce(direction * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if(!isTarget)
            rigidBody.velocity = direction * speed;
        else
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void Setup(Vector3 direction, Transform target = null)
    {
        rigidBody = GetComponent<Rigidbody>();
        canMoving = true;
        this.direction = direction;
        this.target = target;
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
