using UnityEngine;

public class Zoom : MonoBehaviour
{
	private const float ZoomSpeed = 1f;
	private const float Min = 1f;
	private const float Max = 10f;

	private Camera _camera;

	private void Start()
	{
		_camera = GetComponent<Camera>();
	}

	private void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			_camera.orthographicSize += ZoomSpeed;
		}
		else if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			_camera.orthographicSize -= ZoomSpeed;
		}

		_camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, Min, Max);
	}
}
