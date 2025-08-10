using UnityEngine;
using UnityEngine.InputSystem;


namespace _Project.Prototyping
{
	public class BuildCursor : MonoBehaviour
	{
		[SerializeField] private Grid      _grid;
		[SerializeField] private Camera    _camera;
		[SerializeField] private Transform _cursorTransform;
		[SerializeField] private float     _smoothSpeed = 1.0f;
		[SerializeField] private Vector3   _offset;

		private Plane _groundPlane = new(Vector3.up, Vector3.zero);

		private Vector3 _cursorTargetPosition;
		private float   _dampingTime;
		private Vector3 _dampingVelocity;

		private void Awake ()
		{
			_dampingTime = 1.0f / _smoothSpeed;
		}

		private void Update ()
		{
			Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

			if (_groundPlane.Raycast(ray, out float distance))
			{
				Vector3    hitPoint       = ray.GetPoint(distance);
				Vector3Int cellCoord      = _grid.WorldToCell(hitPoint);
				Vector3    cursorPosition = _grid.GetCellCenterWorld(cellCoord);

				_cursorTargetPosition = cursorPosition + _offset;
			}

			_cursorTransform.position = Vector3.SmoothDamp(
				_cursorTransform.position,
				_cursorTargetPosition,
				ref _dampingVelocity,
				_dampingTime
			);
		}
	}
}
