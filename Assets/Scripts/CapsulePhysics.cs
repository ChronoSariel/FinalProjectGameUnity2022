using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulePhysics : MonoBehaviour
{
    public Rigidbody RB;
    public LayerMask layerMask;
    public Vector3 horizontalVelocity => Vector3.ProjectOnPlane(RB.velocity, RB.transform.up);
    public Vector3 verticalVelocity => Vector3.Project(RB.velocity, RB.transform.up);
    public float verticalSpeed => Vector3.Dot(RB.velocity, RB.transform.up);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            Jump();
    }

    // Player when grounded can jump
    [SerializeField] float jumpForce;

    void Jump()
    {
        if (!ground) return;

        RB.velocity = (Vector3.up * jumpForce)
            + horizontalVelocity;
    }

    // Fixed Updates
    void FixedUpdate()
    {
        Move();

        if (!ground) 
            Gravity();

        if (ground && verticalSpeed < RB.sleepThreshold)
            RB.velocity = horizontalVelocity;
        StartCoroutine(LateFixedUpdateRoutine());
        IEnumerator LateFixedUpdateRoutine()
        {
            yield return new WaitForFixedUpdate();
            LateFixedUpdate();
        }
    }

    // Player moves when keys are pressed
    [SerializeField] float speed;

    void Move()
    {
        RB.velocity = Vector3.ProjectOnPlane((Vector3.right * Input.GetAxis("Horizontal") * speed) + (Vector3.forward * Input.GetAxis("Vertical") *speed), normal)
            + verticalVelocity;
    }

    // Gravity applies when the player is not grounded
    [SerializeField] float gravity;

    void Gravity()
    {
        RB.velocity -= Vector3.up * gravity * Time.deltaTime;
    }

    // Late Fixed Updates
    void LateFixedUpdate()
    {
        Ground();
        Snap();
        if (ground)
            RB.velocity = horizontalVelocity;
    }

    // Checks under the player for ground or slopes
    [SerializeField] float groundDist;
    [SerializeField] bool ground;
    Vector3 point;
    Vector3 normal;

    void Ground()
    {
        float maxDist = Mathf.Max(RB.centerOfMass.y, 0) + (RB.sleepThreshold * Time.fixedDeltaTime);
        if (ground && verticalSpeed < RB.sleepThreshold)
            maxDist += groundDist;
        
        ground = Physics.Raycast(RB.worldCenterOfMass, -RB.transform.up, out RaycastHit hit, maxDist, layerMask, QueryTriggerInteraction.Ignore);
        point = ground ? hit.point : RB.transform.position;
        normal = ground ? hit.normal : Vector3.up;
    }

    // Player snaps to the ground, stairs, and slopes
    void Snap()
    {
        RB.transform.up = normal;
        Vector3 goal = point;
        Vector3 difference = goal - RB.transform.position;
        if (RB.SweepTest(difference, out _, difference.magnitude, QueryTriggerInteraction.Ignore)) return;
        RB.transform.position = goal;
    }
}
