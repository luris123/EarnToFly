using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float launchVelocity = 700f;

    public float cooldownDuration = 0.5f;

    private void Start()
    {
        InvokeRepeating("DropCannonBall", 0f, cooldownDuration);
    }

    void DropCannonBall()
    {
        // Instantiate a new projectile at the spawner's position and rotation
        GameObject ball = Instantiate(projectile, transform.position, transform.rotation);

        // Access the Rigidbody component of the projectile and add a relative force
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();

        ballRigidbody.AddRelativeForce(new Vector3( launchVelocity, 0, 0));
    }
}
 