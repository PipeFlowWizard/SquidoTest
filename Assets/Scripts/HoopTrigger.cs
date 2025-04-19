
using UnityEngine;
using UnityEngine.Events;

public class HoopTrigger : MonoBehaviour
{
    public UnityEvent onGoalScored;
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.velocity.y < 0)
            onGoalScored?.Invoke();
    }
    
}
