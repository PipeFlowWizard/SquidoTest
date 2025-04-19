using UnityEngine;

public class XRRig : MonoBehaviour
{
	[Header("References")]
	public Hand leftHand;
	public Hand rightHand;
	public Head head;

	[SerializeField] public Rigidbody _rigidbody;

	[Header("Movement Parameters")]
	public float speed = 1;
	public float rotationalSpeed = 1;
	
	private Vector3 _displacement;
	private Vector3 _lastPosition;

	public Vector3 displacement => _displacement;

	void FixedUpdate()
	{
		ProcessInput();

		Vector3 position = transform.position;
		_displacement = position - _lastPosition;
		_lastPosition = position;
	}

	private void Update()
	{
		UpdateHead();
		UpdateHands();
	}

	private void ProcessInput()
	{
		Vector2 directionalInput = leftHand.input.thumbstick;
		
		Vector3 forward = head.transform.forward;
		forward.y = 0;
		forward.Normalize();

		Vector3 right = head.transform.right;
		right.y = 0;
		right.Normalize();

		Vector3 step = (forward * directionalInput.y + right * directionalInput.x).normalized;
		
		
		_rigidbody.MovePosition(transform.position + step * (speed * Time.fixedDeltaTime));
		Quaternion rot = Quaternion.AngleAxis(rightHand.input.thumbstick.x * rotationalSpeed * Time.fixedDeltaTime, Vector3.up);
		_rigidbody.MoveRotation(rot * transform.rotation);
	}

	void UpdateHands()
	{
		if (leftHand != null)
		{
			leftHand.UpdateInput(); // manually tell hand to update
		}

		if (rightHand != null)
		{
			rightHand.UpdateInput();
		}
	}

	void UpdateHead()
	{
		if(head != null)
			head.UpdateInput();
	}
}