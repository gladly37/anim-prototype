using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SimpleMovementScript : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    public bool isWalking = false;
    public Rigidbody rb;
    public Animator animator;
    public float punchPower;
    public float punchRadius;
    public Transform punchLoc;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isWalking = true;
            //moveTowardsMouse();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger("m_attack");
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isWalking = false;
        }

        if (!isWalking)
        {
            animator.SetBool("m_running", false);
            animator.speed = 1;
            rb.velocity = new Vector3(rb.velocity.x / 2, rb.velocity.y, rb.velocity.z / 2);
        }
    }

    private void FixedUpdate()
    {
        if (isWalking)
        {
            moveTowardsMouse();
        }
    }

    public void moveTowardsMouse()
    {
        animator.SetBool("m_running", true);
        Vector3 target = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Default");
        if (Physics.Raycast(ray,out hit, 1000, layerMask))
        {
            target = hit.point;
        }
        Vector3 force = target- transform.position;
        float distanceBetween = Vector3.Distance(target, new Vector3(transform.position.x, target.y, transform.position.z));
        force = new Vector3(force.x, 0, force.z);
        Debug.Log(distanceBetween);
        //force = transform.TransformDirection(force);
        //rb.AddForce(force.normalized * speed * Time.deltaTime, ForceMode.Force);
        //transform.Translate(force.normalized * speed * Time.deltaTime, Space.Self);
        speed = distanceBetween;
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        animator.speed = speed / 10;
        rb.velocity = force.normalized * speed * 250 * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
        //transform.position += force.normalized * speed * Time.deltaTime;
    }

    public void Punch()
    {
        Collider[] colliders = Physics.OverlapSphere(punchLoc.position, punchRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(punchPower, punchLoc.position, punchRadius);
            }
        }
        
    }
}
