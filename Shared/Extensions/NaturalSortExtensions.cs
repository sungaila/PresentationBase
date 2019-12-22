#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PresentationBase.Extensions
{
	/// <summary>
	/// Provides functions to sort collections of <see cref="string"/>s in a more natural order.
	/// </summary>
	public static class NaturalSortExtensions
	{
		private static readonly Regex _naturalRegex = new Regex(@"\d+", RegexOptions.Compiled);

		/// <summary>
		/// Sorts a given <see cref="string"/> collection in an <strong>ascending</strong> natural order by considering decimal substrings.
		/// </summary>
		/// <typeparam name="T">The colletions generic type.</typeparam>
		/// <param name="items">The collection.</param>
		/// <param name="selector">The selector which converts each item from <paramref name="items"/> into a <see cref="string"/> for comparision.</param>
		/// <param name="stringComparer">An optional <see cref="StringComparer"/> for sorting. Defaults to <see cref="StringComparer.CurrentCulture"/>.</param>
		/// <example>A sorted output could be "Item1", "Item11", "Item105" and "Item2".</example>
		public static IEnumerable<T> OrderByNatural<T>(this IEnumerable<T> items, Func<T, string> selector, StringComparer? stringComparer = null)
		{
			int maxDigits = items
						  .SelectMany(i => _naturalRegex.Matches(selector(i)).Cast<Match>().Select(digitChunk => (int?)digitChunk.Value.Length))
						  .Max() ?? 0;

			return items.OrderBy(i => _naturalRegex.Replace(selector(i), match => match.Value.PadLeft(maxDigits, '0')), stringComparer ?? StringComparer.CurrentCulture);
		}

		/// <summary>
		/// Sorts a given <see cref="string"/> collection in a <strong>descending</strong> natural order by considering decimal substrings.
		/// </summary>
		/// <typeparam name="T">The colletions generic type.</typeparam>
		/// <param name="items">The collection.</param>
		/// <param name="selector">The selector which converts each item from <paramref name="items"/> into a <see cref="string"/> for comparision.</param>
		/// <param name="stringComparer">An optional <see cref="StringComparer"/> for sorting. Defaults to <see cref="StringComparer.CurrentCulture"/>.</param>
		/// <example>A sorted output could be "Item2", "Item105", "Item11" and "Item1".</example>
		public static IEnumerable<T> OrderByNaturalDescending<T>(this IEnumerable<T> items, Func<T, string> selector, StringComparer? stringComparer = null)
		{
			return OrderByNatural(items, selector, stringComparer).Reverse();
		}
	}
}
