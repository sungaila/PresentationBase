using System.Windows.Input;

namespace PresentationBase
{
	/// <summary>
	/// The interface for view model commands.
	/// </summary>
	public interface IViewModelCommand
		: ICommand
	{
		/// <summary>
		/// Informs the view model command that <see cref="ICommand.CanExecute(object)"/> should be reevaluated.
		/// </summary>
		void RaiseCanExecuteChanged();
	}
}
