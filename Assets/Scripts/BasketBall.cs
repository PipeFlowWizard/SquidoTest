using System;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    public AudioSource bounceClip;
    public Grabbable grabbable;
    public TrailRenderer trail;
    public Rigidbody rigidbody;
    private void OnCollisionEnter(Collision other)
    {
        if(bounceClip != null)
            bounceClip.Play();
    }

    public void Update()
    {
        if (grabbable != null)
        {
            if (!grabbable.isGrabbed && rigidbody.velocity.sqrMagnitude > 0.1)
            {
                trail.enabled = true;
            }
            else
            {
                trail.enabled = false;
            }
        }
    }
}
