using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float zoomSpeed = 1f;
    public float followSpeed = 4f;

    public GameObject target;
    public Vector3 offset = new Vector3(0, 0, -10);
    private float zoomLevel = 5f;

    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    public void SetZoom(float zoomLevel)
    {
        this.zoomLevel = zoomLevel;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, followSpeed * Time.deltaTime);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomLevel, zoomSpeed * Time.deltaTime);
            if (Mathf.Abs(camera.orthographicSize - zoomLevel) < 0.01f)
                camera.orthographicSize = zoomLevel;
        }
    }

    private IEnumerator ZoomToLevel(float zoomLevel)
    {
        while (camera.orthographicSize != zoomLevel)
        {

            yield return null;
        }
    }
}
