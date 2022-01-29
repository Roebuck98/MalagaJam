using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;

    void LateUpdate()
    {
        if (Target)
        {
            transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
        }
    }
}
