using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;


namespace Favourites
{
	internal sealed class FavouritesListItem : VisualElement
	{
		private Image _iconImage;
		private Label _nameLabel;

		private bool _isDoubleClicked;

		public event Action<AssetInfo>          RemovalRequested;
		public event Action<FavouritesListItem> Selected;

		public FavouritesListItem ()
		{
			this.AddManipulator(new ContextualMenuManipulator(PopulateContextMenu));

			style.flexDirection = FlexDirection.Row;

			AddIcon();
			AddLabel();

			RegisterCallback<MouseDownEvent>(OnMouseDown);
			RegisterCallback<MouseUpEvent>(OnMouseUp);
			RegisterCallback<MouseMoveEvent>(OnMouseMove);
		}

		public AssetInfo AssetInfo {get; private set;}

		private void AddIcon ()
		{
			_iconImage = new Image {
				pickingMode = PickingMode.Ignore,
				style = {
					width      = 16,
					height     = 16,
					flexShrink = 0
				}
			};

			Add(_iconImage);
		}

		private void AddLabel ()
		{
			_nameLabel = new Label { pickingMode = PickingMode.Ignore };
			Add(_nameLabel);
		}

		public void SetAsset (Object asset)
		{
			if (asset == null)
				return;

			AssetInfo = new AssetInfo(asset);

			_iconImage.image = AssetInfo.Icon;
			_nameLabel.text  = AssetInfo.Asset.name;
		}

		private void PopulateContextMenu (ContextualMenuPopulateEvent @event)
		{
			DropdownMenu dropdownMenu = @event.menu;

			dropdownMenu.AppendAction("Properties...",    ShowProperties);
			dropdownMenu.AppendAction("Show in Explorer", ShowInExplorer);
			dropdownMenu.AppendSeparator();
			dropdownMenu.AppendAction("Remove", Remove);
		}

		private void OnMouseDown (MouseDownEvent @event)
		{
			if (@event.button == (int)MouseButton.MiddleMouse)
			{
				ShowProperties(null);
				return;
			}

			if (@event.button != (int)MouseButton.LeftMouse)
				return;

			if (@event.clickCount > 1)
				_isDoubleClicked = true;
		}

		private void OnMouseUp (MouseUpEvent @event)
		{
			if (@event.clickCount > 1 || @event.button != (int)MouseButton.LeftMouse)
				return;

			if (_isDoubleClicked)
			{
				Choose();
				return;
			}

			Select();
		}

		private void OnMouseMove (MouseMoveEvent @event)
		{
			if (@event.button != (int)MouseButton.LeftMouse || Event.current.type != EventType.MouseDrag)
				return;

			DragAndDrop.PrepareStartDrag();
			DragAndDrop.objectReferences = new[] { AssetInfo.Asset };
			DragAndDrop.StartDrag(_nameLabel.text);
		}

		private void Select ()
		{
			if (Utilities.TryOpenFolderInProjectBrowser(AssetInfo))
				return;

			Selection.activeObject = AssetInfo.Asset;

			Selected?.Invoke(this);

			EditorGUIUtility.PingObject(Selection.activeObject);
		}

		private void Choose ()
		{
			Select();

			int    assetId   = AssetInfo.InstanceId;
			string assetPath = AssetInfo.Path;

			if (AssetInfo.IsFolder)
			{
				EditorUtility.RevealInFinder(assetPath);
			} else
			{
				AssetDatabase.OpenAsset(assetId);
			}

			_isDoubleClicked = false;
		}

		private void ShowProperties (DropdownMenuAction _)
		{
			Object[] currentSelection = Selection.objects;

			Selection.objects = new[] { AssetInfo.Asset };
			EditorApplication.ExecuteMenuItem("Assets/Properties...");

			Selection.objects = currentSelection;
		}

		private void ShowInExplorer (DropdownMenuAction _)
		{
			Object[] currentSelection = Selection.objects;

			Selection.objects = new[] { AssetInfo.Asset };
			EditorApplication.ExecuteMenuItem("Assets/Show in Explorer");

			Selection.objects = currentSelection;
		}

		private void Remove (DropdownMenuAction _)
		{
			const string dialogTitle   = "Remove from Favourites?";
			string       dialogMessage = $"{AssetInfo.Path}\nYou cannot undo this action.";

			if (EditorUtility.DisplayDialog(dialogTitle, dialogMessage, "Yes", "No") == false)
				return;

			RemovalRequested?.Invoke(AssetInfo);
		}
	}
}
