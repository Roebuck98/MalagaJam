using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public CameraTarget Target;
    public float DistanceMultiplier = 1f;
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (Target)
        {
            transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);
            cam.orthographicSize = Mathf.Clamp(Target.Distance * DistanceMultiplier,5f,6.5f);
        }
    }
}
