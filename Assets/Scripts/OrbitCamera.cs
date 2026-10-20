using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private const float MinDistance = 15f, MaxDistance = 45f;
    private const float RotationSpeed = 200f, ZoomSpeed = 15f;
    private const float MinPitch = -10f, MaxPitch = 70f;

    private float _yaw;
    private float _distance = 25f;
    private float _pitch = 33f;

    private void Start()
    {
        if (target == null)
        {
            target = new GameObject("CameraTarget").transform;
            target.position = Vector3.zero;
        }

        UpdateCameraPosition();
    }

    private void LateUpdate()
    {
        HandleRotation();
        HandleZoom();
        UpdateCameraPosition();
    }

    private void HandleRotation()
    {
        if (!Input.GetMouseButton(1)) return;

        _yaw += Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;
        _pitch -= Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, MinPitch, MaxPitch);
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) < 0.001f) return;

        _distance -= scroll * ZoomSpeed;
        _distance = Mathf.Clamp(_distance, MinDistance, MaxDistance);
    }

    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);
        Vector3 offset = rotation * new Vector3(0f, 0f, -_distance);

        transform.position = target.position + offset;
        transform.rotation = rotation;
    }
}