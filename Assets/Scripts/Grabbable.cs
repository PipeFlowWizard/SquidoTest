using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour
{
    public List<Grabber> grabbers;
    public AnimationCurve velocityCurve = AnimationCurve.Constant(0,100,1);
    public UnityEvent onRelease;
    public UnityEvent onGrab;
    
    private Vector3 _displacement;
    private Vector3 _lastPosition;
    public bool isGrabbed => grabbers.Count > 0;


    [SerializeField]
    private Rigidbody _rigidbody;

   public void FixedUpdate()
    {
        if (isGrabbed)
        {
            
            Vector3 averageGrabPosition = Vector3.zero;
            foreach (var grabber in grabbers)
            {
                averageGrabPosition += grabber.transform.position;
            }

            averageGrabPosition /= grabbers.Count;


            if (_rigidbody != null)
            {
                _rigidbody.MovePosition(averageGrabPosition + grabbers[0].hand.rig._rigidbody.velocity);
            }
        }

        _displacement = transform.position - _lastPosition;
        _lastPosition = transform.position;
    }

    public void OnGrab(Grabber grabber)
    {
        if(!grabbers.Contains(grabber))
            grabbers.Add(grabber);
        
        if(isGrabbed && _rigidbody != null)
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            onGrab?.Invoke();
        }
    }

    public void OnRelease(Grabber grabber)
    {
        if(_rigidbody != null && grabbers.Count == 1)
        {
            _rigidbody.useGravity = true;

            var velocity = grabber.transform.parent.TransformVector(grabber.hand.input.velocity);
            var angularVelocity = grabber.transform.TransformVector(grabber.hand.input.angularVelocity);
            
            var cross = Vector3.Cross( _displacement, angularVelocity) * 0.0075f;
            var intermediateVelocity = velocity + cross + grabber.hand.rig.displacement; 
            _rigidbody.velocity = intermediateVelocity * velocityCurve.Evaluate(intermediateVelocity.magnitude); 
            _rigidbody.angularVelocity = angularVelocity;
            onRelease?.Invoke();
        }
        grabbers.Remove(grabber);
    }
}