using UnityEngine;
using UnityEngine.Events;

public class Grabber : MonoBehaviour
{
	public Hand hand;

	public bool isGrapping = false;
	
	public float grabRadius = 0.1f;
	public LayerMask layerMask;
    
	public Grabbable currentGrabbable;
	
	public UnityEvent onRelease;
	public UnityEvent onGrab;

	public void FixedUpdate()
	{
		if (hand.input.triggerPressed && !isGrapping)
		{
			Grab();
			isGrapping = true;
		}
		else if (!hand.input.triggerPressed && isGrapping)
		{
			isGrapping = false;
			if(currentGrabbable != null)
				Release();
		}
	}

	public void Grab()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, grabRadius,layerMask); 
		if(colliders.Length > 0)
		{
			Grabbable grabbable = colliders[0].GetComponent<Grabbable>();

			if (grabbable != null && !grabbable.grabbers.Contains(this))
			{
				currentGrabbable = grabbable;
				grabbable.OnGrab(this);
			}

			//collider.enabled = false;
			
			hand.PlayHapticImpulse(1,0.1f);
			onGrab?.Invoke();

		}
	}
    
	public void Release()
	{
		currentGrabbable.OnRelease(this);
		currentGrabbable = null;
		//collider.enabled = true;
		
		hand.PlayHapticImpulse(1,0.1f);
		onRelease?.Invoke();
	}
	
}