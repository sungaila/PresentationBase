#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PresentationBase
{
	/// <summary>
	/// The base implementation of every view model.
	/// </summary>
	public abstract class ViewModel
		: INotifyPropertyChanged, INotifyDataErrorInfo
	{
		public ViewModel()
		{
			// add all matching commands found with reflection
			AddCommands(KnownCommands.Where(cmd => cmd.GetType().BaseType!.GenericTypeArguments.First().IsAssignableFrom(GetType())).ToArray());
		}

		/// <summary>
		/// Implementation of <see cref="INotifyPropertyChanged.PropertyChanged"/>.
		/// Is used to support bindings between views and view model properties.
		/// </summary>
		public event PropertyChangedEventHandler? PropertyChanged;

		/// <summary>
		/// Implementation of <see cref="INotifyDataErrorInfo.ErrorsChanged"/>.
		/// Is used to validate bound properties.
		/// </summary>
		public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event for the given property name.
		/// </summary>
		/// <param name="propertyName">The name of the property which has been changed. When omitted the property name will be the member name of the caller (which is ideally the view model property).</param>
		protected void RaisePropertyChanged([CallerMemberName]string? propertyName = null)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				Debug.Fail($"{nameof(ViewModel)}.{nameof(ViewModel.RaisePropertyChanged)} has been called with a null or empty {nameof(propertyName)}.");
				return;
			}

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Checks if the old value and new value differ.
		/// If both values are unequal, then the new value is set and <see cref="RaisePropertyChanged"/> is called.
		/// Call this method whenever a view model property has changed (and bound views must notice).
		/// </summary>
		/// <typeparam name="T">The type of the changed property.</typeparam>
		/// <param name="propertyField">The property field which contains the old value.</param>
		/// <param name="newValue">The new value to set.</param>
		/// <param name="propertyValidation">An optional function used for validation of the changed property. It must return a collection of error messages.</param>
		/// <param name="propertyName">The name of the property which has been changed. When omitted the property name will be the member name of the caller (which is ideally the view model property).</param>
		/// <returns>Returns <see cref="true"/> when the value was set.</returns>
		protected bool SetProperty<T>(ref T propertyField, T newValue, Func<T, IEnumerable<string>>? propertyValidation = null, [CallerMemberName]string? propertyName = null)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				Debug.Fail($"{nameof(ViewModel)}.{nameof(SetProperty)} has been called with a null or empty {nameof(propertyName)}.");
				return false;
			}

			AddValidationErrors(propertyName!, propertyValidation?.Invoke(newValue));

			if (EqualityComparer<T>.Default.Equals(propertyField, newValue))
				return false;

			propertyField = newValue;

			if (propertyName != nameof(ParentViewModel) && typeof(ViewModel).IsAssignableFrom(typeof(T)) && newValue != null)
				(newValue as ViewModel)!.ParentViewModel = this;

			RaisePropertyChanged(propertyName);

			if (propertyName != nameof(IsDirty) && !IgnoredDirtyProperties.Contains(propertyName))
			{
				IsDirty = true;
				if (ParentViewModel != null)
				{
					ParentViewModel.IsDirty = true;
				}
			}

			return true;
		}

		private ViewModel? _parentViewModel;

		/// <summary>
		/// The logical parent of this view model.
		/// </summary>
		/// <remarks>Every view model can have one parent only.</remarks>
		public ViewModel? ParentViewModel
		{
			get => _parentViewModel;
			set => SetProperty(ref _parentViewModel, value);
		}

		/// <summary>
		/// The top most parent of this view model.
		/// </summary>
		public ViewModel? RootViewModel
		{
			get
			{
				ViewModel? parent = ParentViewModel;

				while (parent != null)
				{
					if (parent.ParentViewModel == null)
						return parent;

					parent = parent.ParentViewModel;
				}

				return null;
			}
		}

		/// <summary>
		/// Adds commands to this view model.
		/// This ensures that <see cref="ICommand.CanExecute(object)"/> is called whenever a property was changed.
		/// </summary>
		/// <param name="commands"></param>
		private void AddCommands(params IViewModelCommand[] commands)
		{
			foreach (IViewModelCommand command in commands)
			{
				if (command == null)
					continue;

				Type key = command.GetType();

				if (key == null || Commands.ContainsKey(key))
					continue;

				Commands.Add(key, command);
				PropertyChanged += (sender, e) => command.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// Removes existing commands for this view model.
		/// </summary>
		/// <param name="commands"></param>
		private void RemoveCommands(params IViewModelCommand[] commands)
		{
			foreach (IViewModelCommand command in commands)
			{
				if (command == null)
					continue;

				Type key = command.GetType();

				if (key == null || !Commands.ContainsKey(key))
					continue;

				Commands.Remove(key);
				PropertyChanged -= (sender, e) => command.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// A dictionary filled with commands for this view model. The key is the <see cref="Type"/> of the command.
		/// </summary>
		public Dictionary<Type, IViewModelCommand> Commands { get; } = new Dictionary<Type, IViewModelCommand>();

		/// <summary>
		/// A multi purpose <see cref="object"/> tag for this view model.
		/// Use it for e.g. identifying this view model.
		/// </summary>
		public object? Tag { get; set; }

		private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

		/// <summary>
		/// If any property has failed validation.
		/// </summary>
		public bool HasErrors => _errors.Any();

		/// <summary>
		/// If there a no errors for this ViewModel.
		/// It is the inverse to <see cref="HasErrors"/>.
		/// Can be overwritten e.g. if children must be valid.
		/// </summary>
		public virtual bool IsValid => !HasErrors;

		/// <summary>
		/// Returns all errors for a given property name.
		/// </summary>
		/// <param name="propertyName">The property name.</param>
		public IEnumerable? GetErrors(string propertyName)
		{
			return !string.IsNullOrEmpty(propertyName) && _errors.ContainsKey(propertyName)
				? _errors[propertyName]
				: null;
		}

		private void ClearErrors(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
				return;

			_errors.Remove(propertyName);
			OnErrorsChanged(propertyName);
		}

		private void AddError(string propertyName, string errorMessage)
		{
			if (!_errors.ContainsKey(propertyName))
			{
				_errors[propertyName] = new List<string>();
			}

			if (!_errors[propertyName].Contains(errorMessage))
			{
				_errors[propertyName].Add(errorMessage);
				OnErrorsChanged(propertyName);
			}
		}

		private void AddValidationErrors(string propertyName, IEnumerable<string>? errorMessages)
		{
			if (string.IsNullOrEmpty(propertyName))
				return;

			ClearErrors(propertyName);

			if (errorMessages == null)
				return;

			foreach (string errorMessage in errorMessages)
			{
				AddError(propertyName, errorMessage);
			}
		}

		private void OnErrorsChanged(string propertyName)
		{
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
			RaisePropertyChanged(nameof(HasErrors));
			RaisePropertyChanged(nameof(IsValid));
		}

		/// <summary>
		/// A collection of property names which do not set the <see cref="IsDirty"/> flag when changed.
		/// </summary>
		protected virtual IEnumerable<string> IgnoredDirtyProperties { get => new List<string>(); }

		private bool _isDirty;

		/// <summary>
		/// Indicates that there are changes made to this <see cref="ViewModel"/> since its creation.
		/// </summary>
		public bool IsDirty
		{
			get => _isDirty;
			set => SetProperty(ref _isDirty, value);
		}

		private bool _isRefreshing;

		/// <summary>
		/// Indicates that this <see cref="ViewModel"/> is changing and others should avoid interfering.
		/// </summary>
		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		/// <summary>
		/// A list containing all known commands found with reflection.
		/// </summary>
		private static readonly List<IViewModelCommand> KnownCommands = new List<IViewModelCommand>();

		static ViewModel()
		{
			foreach (var commandType in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
				.Where(type => typeof(IViewModelCommand).IsAssignableFrom(type) && !type.IsAbstract && type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GenericTypeArguments.Length == 1))
			{
				KnownCommands.Add((IViewModelCommand)Activator.CreateInstance(commandType)!);
			}
		}
	}
}
