using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovment : MonoBehaviour
{
    public float speed = 3;
    public float gravity = 3;
    public Transform container;
    public float thrust;
    public Rigidbody rb;
    float fakeAcceleration = 2f;
    Transform targetPlanet;
    public Transform everything;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPlanet = container.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        if(input.x > 0)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }else if (input.x < 0)
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        float rotationAngle = 0;
        Vector3 differnece = transform.position - targetPlanet.position;
        rotationAngle = Mathf.Atan2(differnece.x, differnece.y);

        transform.rotation = Quaternion.Euler(0, 0, -rotationAngle*Mathf.Rad2Deg);
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = transform.up * thrust;
        }

        if (Vector3.Magnitude(transform.position - targetPlanet.position) < targetPlanet.lossyScale.y / 2 + 0.2f)
        {
            fakeAcceleration = 2f;
        }
        else
        {
            fakeAcceleration = fakeAcceleration+ 0.2f;
        }
        foreach (Transform child in container)
        {
            if (Vector3.Magnitude(transform.position-child.position)< Vector3.Magnitude(transform.position - targetPlanet.position))
            {
                targetPlanet = child;
            }
        }
        if (Vector3.Magnitude(targetPlanet.position) > 0.1f)
        {
            everything.position -= targetPlanet.position.normalized * 4f*Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        rb.velocity = Vector3.MoveTowards(rb.velocity, -(transform.position - targetPlanet.position).normalized * gravity,fakeAcceleration*Time.fixedDeltaTime);
    }
}
