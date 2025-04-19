using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Head : MonoBehaviour
{
	public XRNode headNode = XRNode.CenterEye;
	private InputDevice _device;
	
	private void Start()
	{
		InitializeDevice();
	}

	public void Update()
	{
		if (!_device.isValid)
			InitializeDevice();
	}

	private void InitializeDevice()
	{
		var devices = new List<InputDevice>();
		InputDevices.GetDevicesAtXRNode(headNode, devices);

		if (devices.Count > 0)
			_device = devices[0];
	}

	public void UpdateInput()
	{
		if (!_device.isValid)
			InitializeDevice();
		
		_device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
		_device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);
		
		transform.SetLocalPositionAndRotation(position, rotation);
	}
}