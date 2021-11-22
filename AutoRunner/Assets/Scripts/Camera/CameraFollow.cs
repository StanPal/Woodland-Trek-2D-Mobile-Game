using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform FollowTransform;

    private void FixedUpdate()
    {
        this.transform.position = new Vector3(FollowTransform.position.x, FollowTransform.position.y, this.transform.position.z);
    }

}
