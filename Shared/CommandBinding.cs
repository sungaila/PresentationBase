#nullable enable
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace PresentationBase
{
	/// <summary>
	/// A markup extension to unify <see cref="ViewModelCommand{TViewModel}"/> bindings.
	/// </summary>
	[MarkupExtensionReturnType(typeof(ICommand))]
	[ContentProperty(nameof(CommandType))]
	public class CommandBinding
		: Binding
	{
		/// <summary>
		/// The command type.
		/// </summary>
		[ConstructorArgument("commandType")]
		public Type CommandType { get; set; }

		/// <summary>
		/// Creates a new <see cref="CommandBinding"/> instance.
		/// </summary>
		/// <param name="commandType">The type of the <see cref="ViewModelCommand{TViewModel}"/>.</param>
		public CommandBinding(Type commandType)
		{
			CommandType = commandType ?? throw new ArgumentNullException(nameof(commandType));
			Path = new PropertyPath("Commands[(0)]", CommandType);
			Mode = BindingMode.OneWay;
		}
	}
}
