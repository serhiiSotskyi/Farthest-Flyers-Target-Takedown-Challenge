using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform pov;
    [SerializeField] float speed;
    public Transform plane;
    private Vector3 target;

    private void Update()
    {
        if(plane !=null) target = pov.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.forward = pov.forward;
    }
}
