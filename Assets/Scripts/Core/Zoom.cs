using UnityEngine;
using UnityEngine.InputSystem;

public class Zoom : MonoBehaviour
{
	private const float ZoomSpeed = 1f;
	private const float Min = 1f;
	private const float Max = 32f;

	private Camera _camera;

	private void Start()
	{
		_camera = GetComponent<Camera>();
	}

	private void Update()
	{
		if (GameObject.FindWithTag("Window") == null && GameObject.FindWithTag("Popup") == null)
		{
			if (Mouse.current.scroll.ReadValue().y < 0)
			{
				_camera.orthographicSize += ZoomSpeed;
			}
			else if (Mouse.current.scroll.ReadValue().y > 0)
			{
				_camera.orthographicSize -= ZoomSpeed;
			}

			_camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, Min, Max);
		}
	}
}
