using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace PresentationBase
{
	/// <summary>
	/// The base implementation of commands for view models.
	/// </summary>
	/// <typeparam name="TViewModel">The type of the view model.</typeparam>
	public abstract class ViewModelCommand<TViewModel>
		: IViewModelCommand
		where TViewModel : ViewModel
	{
		public event EventHandler CanExecuteChanged;

		bool ICommand.CanExecute(object parameter)
		{
			if (!(parameter is TViewModel viewModel))
				return true;

			return CanExecute(viewModel);
		}

		void ICommand.Execute(object parameter)
		{
			if (!(parameter is TViewModel viewModel))
				return;

			Execute(viewModel);
		}

		/// <summary>
		/// Returns if the command can be executed for the given view model.
		/// </summary>
		/// <param name="parameter">The view model this command would be executed on.</param>
		/// <returns>Returns if <see cref="Execute(TViewModel)"/> is allowed for the given <paramref name="parameter"/>.</returns>
		public virtual bool CanExecute(TViewModel parameter)
		{
			return parameter != null;
		}

		/// <summary>
		/// Executes the command for the given view model.
		/// </summary>
		/// <param name="parameter">The view model this command is executed on.</param>
		public abstract void Execute(TViewModel parameter);

		public void RaiseCanExecuteChanged()
		{
			if (Application.Current == null)
				return;

			Application.Current.Dispatcher.Invoke(() => CanExecuteChanged?.Invoke(this, EventArgs.Empty), DispatcherPriority.Send);
		}
	}
}
