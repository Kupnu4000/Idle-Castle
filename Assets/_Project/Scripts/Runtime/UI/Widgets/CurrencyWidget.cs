using IdleCastle.Runtime.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace IdleCastle.Runtime.UI.Widgets
{
	public class CurrencyWidget : MonoBehaviour
	{
		[SerializeField] private Image    _icon;
		[SerializeField] private TMP_Text _titleText;
		[SerializeField] private TMP_Text _amountText;

		public void Initialize (CurrencyConfig config)
		{
			_icon.sprite    = config.Icon;
			_titleText.text = config.Title;
		}

		public void SetValue (double value)
		{
			// TODO: добавить форматирование в зависимости от типа валюты
			_amountText.text = value.ToString("N0");
		}
	}
}
