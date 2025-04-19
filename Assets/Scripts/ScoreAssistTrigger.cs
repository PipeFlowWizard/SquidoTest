using UnityEngine;

public class ScoreAssistTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform point;

    [SerializeField]
    private float strength = 5f;


    private void OnTriggerStay(Collider other)
    {
        if (other != null && other.attachedRigidbody != null)
        {
            other.attachedRigidbody.velocity += (point.position - other.transform.position) * strength * Time.fixedDeltaTime;
        }
    }
}
