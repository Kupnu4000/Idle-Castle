using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace _Project.Prototyping
{
	// TODO Refactor: это необязательно должно быть MonoBehaviour, можно использовать InputSystem_Actions напрямую
	public class MouseInputRouter : MonoBehaviour, InputSystem_Actions.IMouseActions
	{
		private InputSystem_Actions              _actions;
		private InputSystem_Actions.MouseActions _mouse;

		private InputAction _moveInputAction;

		public event Action<InputAction.CallbackContext> MoveInputAction
		{
			add => _moveInputAction.performed += value;
			remove => _moveInputAction.performed -= value;
		}

		private void Awake ()
		{
			_actions = new InputSystem_Actions();
			_mouse   = _actions.Mouse;
			_mouse.AddCallbacks(this);

			_moveInputAction = _mouse.Move;

			MoveInputAction += HandleMove;
		}

		private void HandleMove (InputAction.CallbackContext context)
		{
			Vector2 position = context.ReadValue<Vector2>();

			Debug.Log($"position = {position}");
		}

		private void Start ()
		{
			_mouse.Enable();
		}

		public void OnMove (InputAction.CallbackContext context)
		{
			// Vector2 position = context.ReadValue<Vector2>();
		}

		private void OnDestroy ()
		{
			_actions.Dispose();
		}
	}
}
