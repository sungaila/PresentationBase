using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace PresentationBase
{
    /// <summary>
    /// An <see cref="ObservableCollection{T}"/> for view models.
    /// It ensures that the <see cref="ViewModel.ParentViewModel"/> property is correctly set when working on collections.
    /// Also adds methods for collection manipulation which are dispatched to <see cref="Application.Current"/>.
    /// </summary>
    /// <typeparam name="TViewModel">The view model type of this collection.</typeparam>
    public class ObservableViewModelCollection<TViewModel>
        : ObservableCollection<TViewModel>
        where TViewModel : ViewModel
    {
        private ViewModel OwnerViewModel { get; }

        /// <summary>
        /// Creates a new <see cref="ObservableCollection{T}"/> instance.
        /// </summary>
        /// <param name="viewModel">The parent view model.</param>
        public ObservableViewModelCollection(ViewModel viewModel)
        {
            OwnerViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            CollectionChanged += ObservableViewModelCollection_CollectionChanged;
        }

        private void ObservableViewModelCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (TViewModel item in e.OldItems.OfType<TViewModel>())
                    item.ParentViewModel = null;
            }

            if (e.NewItems != null)
            {
                foreach (TViewModel item in e.NewItems.OfType<TViewModel>())
                    item.ParentViewModel = OwnerViewModel;
            }
        }

        /// <summary>
        /// Observes the child view models for changes to the properties defined in <paramref name="propertyNames"/>.
        /// When changes are detected then <paramref name="action"/> is invoked.
        /// </summary>
        /// <param name="action">The action to invoke on observed change.</param>
        /// <param name="propertyNames">The properties to observe for changes.</param>
        public void Observe(Action action, params string[] propertyNames)
        {
            if (action == null || propertyNames == null)
                return;

            void propertyChangedHandler(object sender, PropertyChangedEventArgs e)
            {
                if (!propertyNames.Contains(e.PropertyName))
                    return;

                action.Invoke();
            }

            void collectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems.OfType<ViewModel>())
                        item.PropertyChanged -= propertyChangedHandler;
                }

                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems.OfType<ViewModel>())
                        item.PropertyChanged += propertyChangedHandler;
                }
            }

            CollectionChanged -= collectionChangedHandler;
            CollectionChanged += collectionChangedHandler;

            foreach (ViewModel item in this)
            {
                item.PropertyChanged -= propertyChangedHandler;
                item.PropertyChanged += propertyChangedHandler;
            }
        }

        /// <summary>
        /// Adds a view model to the end of the collection.
        /// This method is dispatched to <see cref="Application.Current"/>.
        /// </summary>
        /// <param name="item">The view model to add.</param>
        new public void Add(TViewModel item)
        {
            Application.Current.Dispatcher.Invoke(() => base.Add(item), DispatcherPriority.Send);
        }

        /// <summary>
        /// Adds multiple view models to the end of the collection.
        /// This method is dispatched to <see cref="Application.Current"/>.
        /// </summary>
        /// <param name="collection">The view models to add.</param>
        public void AddRange(IEnumerable<TViewModel> collection)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in collection)
                {
                    base.Add(item);
                }
            }, DispatcherPriority.Send);
        }

        /// <summary>
        /// Clears the collection and adds the given view models.
        /// This method is dispatched to <see cref="Application.Current"/>.
        /// </summary>
        /// <param name="collection">The replacement view models.</param>
        public void Replace(params TViewModel[] collection)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                base.Clear();
                foreach (var item in collection)
                {
                    base.Add(item);
                }
            }, DispatcherPriority.Send);
        }

        /// <summary>
        /// Clears the collection.
        /// This method is dispatched to <see cref="Application.Current"/>.
        /// </summary>
        new public void Clear()
        {
            Application.Current.Dispatcher.Invoke(() => base.Clear(), DispatcherPriority.Send);
        }

        /// <summary>
        /// Inserts a view model at the given <paramref name="index"/> to the collection.
        /// This method is dispatched to <see cref="Application.Current"/>.
        /// </summary>
        /// <param name="index">The index at which the view model is inserted.</param>
        /// <param name="item">The view model to insert.</param>
        new public void Insert(int index, TViewModel item)
        {
            Application.Current.Dispatcher.Invoke(() => base.Insert(index, item), DispatcherPriority.Send);
        }

        /// <summary>
        /// Removes a view model from the collection.
        /// This method is dispatched to <see cref="Application.Current"/>.
        /// </summary>
        /// <param name="item">The view model to remove.</param>
        /// <returns>Returns <c>true</c> if the view model was found and removed. Returns <c>false</c> otherwise.</returns>
        new public bool Remove(TViewModel item)
        {
            bool result = false;
            Application.Current.Dispatcher.Invoke(() => { result = base.Remove(item); }, DispatcherPriority.Send);
            return result;
        }

        /// <summary>
        /// Removes a view model at the given <paramref name="index"/>.
        /// This method is dispatched to <see cref="Application.Current"/>.
        /// </summary>
        /// <param name="index">The index of the view model to remove.</param>
        new public void RemoveAt(int index)
        {
            Application.Current.Dispatcher.Invoke(() => base.RemoveAt(index), DispatcherPriority.Send);
        }
    }
}
