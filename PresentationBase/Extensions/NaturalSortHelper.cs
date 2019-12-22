using System;
using System.Text.RegularExpressions;

namespace PresentationBase.Extensions
{
	/// <summary>
	/// Allows to convert a given property from a view model into a <see cref="string"/> for sorting.
	/// The returned <see cref="string"/> pads leading zeros to any found decimal substring.
	/// This is useful when a natual sort is needed that should integrate into WPFs binding system without much effort.
	/// </summary>
	public class NaturalSortHelper
	{
		/// <summary>
		/// The parent view model.
		/// </summary>
		public ViewModel ParentViewModel { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="NaturalSortHelper"/>.
		/// </summary>
		/// <param name="viewModel">The parent view model.</param>
		public NaturalSortHelper(ViewModel viewModel)
		{
			ParentViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
		}

		private static readonly Regex _paddingRegex = new Regex(@"\d+", RegexOptions.Compiled);

		/// <summary>
		/// Takes a property name of the <see cref="ParentViewModel"/>.
		/// Returns a <see cref="string"/> with leading zeros for any decimal substring found.
		/// </summary>
		/// <param name="propertyName">The name of an existing property of <see cref="ParentViewModel"/>.</param>
		public string this[string propertyName]
		{
			get
			{
				if (ParentViewModel == null)
					return null;

				var propertyInfo = ParentViewModel.GetType().GetProperty(propertyName);
				if (propertyInfo == null)
					return null;

				var propertyValue = propertyInfo.GetValue(ParentViewModel);
				if (propertyValue == null)
					return null;

				var propertyValueAsString = propertyValue.ToString();
				if (propertyValueAsString == null)
					return null;

				return _paddingRegex.Replace(propertyValueAsString, m => m.Value.PadLeft(20, '0'));
			}
		}
	}
}
