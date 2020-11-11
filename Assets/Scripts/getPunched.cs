using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPunched : MonoBehaviour
{
    public Transform punchLoc;
    public Rigidbody rb;
    public float force;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Punch()
    {
        rb.AddExplosionForce(force, punchLoc.position, radius);
    }
}
