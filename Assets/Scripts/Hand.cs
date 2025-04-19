using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

[System.Serializable]
public struct HandInput
{
    public Vector3 position;
    public Quaternion rotation;

    public Vector3 velocity;
    public Vector3 angularVelocity;

    public Vector3 acceleration;
    public Vector3 angularAcceleration;

    public Vector2 thumbstick;
    public bool thumbstickClick;

    public float trigger;
    public bool triggerPressed;

    public float grip;
    public bool gripPressed;

    public bool primaryButton;
    public bool secondaryButton;
    public bool menuButton;
}

public class Hand : MonoBehaviour
{
    public XRNode handNode;
    public HandInput input;
    public XRRig rig;
    
    private InputDevice _device;
    void Start()
    {
        InitializeDevice();
    }

    void InitializeDevice()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(handNode, devices);

        if (devices.Count > 0)
            _device = devices[0];
    }

    void Update()
    {
        if (!_device.isValid)
            InitializeDevice();
    }

    public void UpdateInput()
    {
        if (!_device.isValid)
            InitializeDevice();

        input = new HandInput();

        // PlaySpace
        _device.TryGetFeatureValue(CommonUsages.devicePosition, out input.position);
        _device.TryGetFeatureValue(CommonUsages.deviceRotation, out input.rotation);

        // Motion
        _device.TryGetFeatureValue(CommonUsages.deviceVelocity, out input.velocity);
        _device.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out input.angularVelocity);
        _device.TryGetFeatureValue(CommonUsages.deviceAcceleration, out input.acceleration);
        _device.TryGetFeatureValue(CommonUsages.deviceAngularAcceleration, out input.angularAcceleration);

        // Thumbstick
        _device.TryGetFeatureValue(CommonUsages.primary2DAxis, out input.thumbstick);
        _device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out input.thumbstickClick);

        // Trigger
        _device.TryGetFeatureValue(CommonUsages.trigger, out input.trigger);
        _device.TryGetFeatureValue(CommonUsages.triggerButton, out input.triggerPressed);

        // Grip
        _device.TryGetFeatureValue(CommonUsages.grip, out input.grip);
        _device.TryGetFeatureValue(CommonUsages.gripButton, out input.gripPressed);

        // Buttons
        _device.TryGetFeatureValue(CommonUsages.primaryButton, out input.primaryButton);
        _device.TryGetFeatureValue(CommonUsages.secondaryButton, out input.secondaryButton);
        _device.TryGetFeatureValue(CommonUsages.menuButton, out input.menuButton);
        
        transform.SetLocalPositionAndRotation(input.position,input.rotation);
    }
    
    public void PlayHapticImpulse(float amplitude, float duration)
    {
        if (_device.isValid)
        {
            HapticCapabilities capabilities;
            if (_device.TryGetHapticCapabilities(out capabilities) && capabilities.supportsImpulse)
            {
                // channel 0 is standard for most controllers
                _device.SendHapticImpulse(0, amplitude, duration);
            }
        }
    }
}
