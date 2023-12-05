using UnityEngine;

public class Skateboard : Interactable
{
    private Transform playerTransform;
    public float moveForceMagnitude = 500f; // Adjust as needed
    public float moveDuration = 2.0f; // Duration for which the skateboard moves
    public float slowDownFactor = 0.5f; // Factor to slow down the skateboard
    public Prompt interactPrompt;

    private Rigidbody rb;
    private bool isMoving = false;
    private float moveTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Freeze position in the left and right directions (assuming X axis)
        // and freeze rotation around all axes
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
    }

    public override void Activate()
    {
        if (!Activated && !isMoving)
        {
            Activated = true;

            // Determine the direction from the skateboard to the player
            Vector3 directionToPlayer = (transform.position - playerTransform.position).normalized;

            // Correctly align the force direction along the skateboard's world z-axis
            Vector3 worldForward = new Vector3(0, 0, 1); // World forward direction
            Vector3 skateboardForward = transform.rotation * worldForward; // Skateboard's forward direction in world space
            Vector3 forceDirection = Vector3.Dot(skateboardForward, directionToPlayer) > 0 ? -skateboardForward : skateboardForward;

            // Apply force strictly along the z-axis
            Vector3 force = forceDirection * moveForceMagnitude;
            force.x = 0; // Ensure no force is applied along the x-axis

            rb.isKinematic = false;
            rb.AddForce(force, ForceMode.Impulse);

            interactPrompt?.Disable();
            isMoving = true;
            moveTimer = 0f; // Reset the move timer
        }
    }


    void FixedUpdate()
    {
        if (isMoving)
        {
            moveTimer += Time.fixedDeltaTime;

            if (moveTimer >= moveDuration)
            {
                // Gradually slow down the skateboard
                Vector3 newVelocity = rb.velocity * slowDownFactor;
                newVelocity.x = 0; // Ensure no movement along the x-axis
                rb.velocity = newVelocity;

                if (rb.velocity.magnitude < 0.1f) // Threshold to stop the skateboard
                {
                    rb.velocity = Vector3.zero;
                    rb.isKinematic = true; // Make the Rigidbody kinematic to stop it from moving
                    isMoving = false;
                    Activated = false; // Allow reactivation
                }
            }
        }
    }
}