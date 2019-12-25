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
		: IViewModelCommand<TViewModel>
		where TViewModel : ViewModel
	{
		/// <summary>
		/// Raised when changes to the view model were made and <see cref="CanExecute(TViewModel)"/> should be reevaluated.
		/// </summary>
		public event EventHandler? CanExecuteChanged;

		/// <summary>
		/// Implementation of <see cref="ICommand.CanExecute(object)"/>.
		/// The <paramref name="parameter"/> is not cast to <typeparamref name="TViewModel"/> here.
		/// </summary>
		/// <param name="parameter">The view model.</param>
		bool ICommand.CanExecute(object parameter)
		{
			if (!(parameter is TViewModel viewModel))
				return true;

			return CanExecute(viewModel);
		}

		/// <summary>
		/// Implementation of <see cref="ICommand.Execute(object)"/>.
		/// The <paramref name="parameter"/> is not cast to <typeparamref name="TViewModel"/> here.
		/// </summary>
		/// <param name="parameter">The view model.</param>
		void ICommand.Execute(object parameter)
		{
			if (!(parameter is TViewModel viewModel))
				return;

			Execute(viewModel);
		}

		/// <inheritdoc/>
		public virtual bool CanExecute(TViewModel parameter)
		{
			return parameter != null;
		}

		/// <inheritdoc/>
		public abstract void Execute(TViewModel parameter);

		/// <inheritdoc/>
		public void RaiseCanExecuteChanged()
		{
			if (Application.Current == null)
				return;

			Application.Current.Dispatcher.Invoke(() => CanExecuteChanged?.Invoke(this, EventArgs.Empty), DispatcherPriority.Send);
		}
	}
}
