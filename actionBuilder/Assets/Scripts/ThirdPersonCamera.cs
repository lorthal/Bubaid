using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {


    [SerializeField]
    private GameObject cameraPivot;

    public Transform a, b, c, d;

    public float pitchMax = 80f;
    public float pitchMin = -80f;

    void Update ()
    {
        float angle = 0.0f;
        if (Input.GetAxis("Mouse X") != 0)
        {
            float x = 5 * Input.GetAxis("Mouse X");
            cameraPivot.transform.Rotate(0, x, 0, Space.World);
            // m_Character.Move(new Vector3(0, x, 0), Input.GetKey(KeyCode.C), CrossPlatformInputManager.GetButtonDown("Jump"));
        }
        if (Input.GetAxis("Mouse Y") != 0)
        {
            float y = 5 * -Input.GetAxis("Mouse Y");
            angle = Vector3.Angle(transform.forward, Vector3.Scale(transform.forward, new Vector3(1, 0, 1)));
            if (transform.forward.y > 0) angle = -angle;

            y = Mathf.Clamp(y, pitchMin - angle, pitchMax - angle);

            if ((angle < pitchMax && Input.GetAxis("Mouse Y") < 0) || (angle > pitchMin && Input.GetAxis("Mouse Y") > 0))
            {
                cameraPivot.transform.Rotate(y, 0, 0, Space.Self);
            }

            float difference = pitchMax - pitchMin;
            angle -= pitchMin;
            float progress = angle / difference;

            transform.position = cameraPivot.transform.position + Bezier(a.localPosition, b.localPosition, c.localPosition, d.localPosition, progress);
        }

    }

    public Vector3 Bezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        float mt = (1 - t);
        float mtmt = mt * mt;
        float tt = t * t;

        return + mt * mtmt * a
             + 3 * mtmt * t * b
             + 3 * mt * tt * c
             + tt * t * d;
    }
}
