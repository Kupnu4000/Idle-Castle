using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;


namespace Favourites
{
	internal class MyNewFavouriteWindow : EditorWindow, IHasCustomMenu
	{
		private readonly Lazy<Favourites> _favourites = new Lazy<Favourites>(Utilities.GetOrCreateFavourites);

		private ListView _listView;

		[MenuItem("Window/My New Favourite")]
		public static void OpenWindow ()
		{
			GetWindow<MyNewFavouriteWindow>("Favourites").Show();
		}

		private void OnEnable ()
		{
			titleContent = new GUIContent(
				"Favourites",
				EditorGUIUtility.IconContent("d_FolderFavorite Icon").image
			);

			_favourites.Value.Modified += OnFavouritesModified;

			CreteListView();
			SetupDragAndDrop();
		}

		private void OnFavouritesModified ()
		{
			_listView.Rebuild();
		}

		private void OnDisable ()
		{
			_favourites.Value.Modified -= OnFavouritesModified;
		}

		private void OnProjectChange ()
		{
			_listView.Rebuild();
		}

		private void OnItemSelected (FavouritesListItem item)
		{
			_listView.ClearSelection();
			int index = _listView.itemsSource.IndexOf(item.AssetInfo);
			_listView.SetSelection(index);
		}

		private void CreteListView ()
		{
			_listView = new ListView(
				_favourites.Value.Assets,
				(int)EditorGUIUtility.singleLineHeight,
				CreateListItem,
				BindListItem
			) {
				selectionType = SelectionType.None,
				style         = { flexGrow = 1f },
				showBorder    = true,
				reorderable   = false
			};

			rootVisualElement.Add(_listView);
		}

		private void OnItemRemovalRequested (AssetInfo assetInfo)
		{
			_favourites.Value.RemoveInternal(assetInfo.Asset);
		}

		private FavouritesListItem CreateListItem ()
		{
			FavouritesListItem listItem = new FavouritesListItem();

			listItem.RemovalRequested += OnItemRemovalRequested;
			listItem.Selected         += OnItemSelected;

			return listItem;
		}

		private void BindListItem (VisualElement element, int index)
		{
			FavouritesListItem listItem = (FavouritesListItem)element;

			Object asset = _favourites.Value[index];

			listItem.SetAsset(asset);
		}

		public void AddItemsToMenu (GenericMenu menu)
		{
			menu.AddItem(
				EditorGUIUtility.TrTempContent("Clear Favourites"),
				false,
				ClearFavourites
			);
		}

		private void ClearFavourites ()
		{
			const string dialogTitle   = "Clear All Favourites?";
			const string dialogMessage = "Are you sure you want to clear all Favourites?\nYou cannot undo this action.";

			if (EditorUtility.DisplayDialog(dialogTitle, dialogMessage, "Yes", "No") == false)
				return;

			_favourites.Value.Clear();
		}

		private void SetupDragAndDrop ()
		{
			_listView.RegisterCallback<DragEnterEvent>(static @event => @event.StopPropagation(), TrickleDown.TrickleDown);
			_listView.RegisterCallback<DragLeaveEvent>(static @event => @event.StopPropagation(), TrickleDown.TrickleDown);

			_listView.RegisterCallback<DragUpdatedEvent>(static @event =>
				{
					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
					@event.StopPropagation();
				},
				TrickleDown.TrickleDown
			);

			_listView.RegisterCallback<DragPerformEvent, Favourites>(static (evt, favourites) =>
				{
					if (favourites != null)
					{
						Object[] assets = DragAndDrop.objectReferences.Where(AssetDatabase.Contains).ToArray();
						favourites.Add(assets);
					}

					evt.StopPropagation();
				},
				_favourites.Value,
				TrickleDown.TrickleDown
			);
		}
	}
}
