using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PresentationBase
{
    /// <summary>
    /// A <see cref="CompositeCollection"/> for view models.
    /// It is strongly typed with <typeparamref name="TViewModel"/> and hides the underlying <see cref="CollectionContainer"/>.
    /// All methods for collection manipulation are dispatched to <see cref="Application.Current"/>.
    /// </summary>
    /// <typeparam name="TViewModel">The view model type of this composite collection.</typeparam>
    public class CompositeViewModelCollection<TViewModel>
        : CompositeCollection
        where TViewModel : ViewModel
    {
        /// <summary>
        /// Creates a new <see cref="CompositeViewModelCollection{T}"/> instance that is empty and has default initial capacity.
        /// </summary>
        public CompositeViewModelCollection() : base() { }

        /// <summary>
        /// Creates a new <see cref="CompositeViewModelCollection{T}"/> instance that is empty and has a specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of collections that the new list is initially capable of storing.</param>
        public CompositeViewModelCollection(int capacity) : base(capacity) { }

        /// <inheritdoc/>
        public IEnumerator<IEnumerable<TViewModel>> GetEnumerator()
        {
            return this.Cast<CollectionContainer>().Select(c => (IEnumerable<TViewModel>)c.Collection).GetEnumerator();
        }

        /// <summary>
        /// Indexer property that retrieves or replaces the collection at the given zero-based offset in the collection.
        /// </summary>
        /// <param name="collectionIndex">The zero-based offset of the collection to retrieve or replace.</param>
        /// <returns>The collection at the specified zero-based offset.</returns>
        new public IEnumerable<TViewModel> this[int collectionIndex]
        {
            get
            {
                IEnumerable<TViewModel>? result = null;

                Dispatcher.Dispatch(() =>
                {
                    result = (IEnumerable<TViewModel>)((CollectionContainer)base[collectionIndex]).Collection;
                });

                return result!;
            }
            set
            {
                Dispatcher.Dispatch(() =>
                {
                    base[collectionIndex] = new CollectionContainer { Collection = value };
                });
            }
        }

        /// <summary>
        /// Adds the specified collection to this composite collection.
        /// </summary>
        /// <param name="collection">New collection to add.</param>
        /// <returns>Zero-based index where the new collection is added.</returns>
        public int Add(IEnumerable<TViewModel> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            int result = 0;

            Dispatcher.Dispatch(() =>
            {
                result = base.Add(new CollectionContainer { Collection = collection });
            });

            return result;
        }

        /// <summary>
        /// Clears the collection
        /// </summary>
        new public void Clear()
        {
            Dispatcher.Dispatch(() => base.Clear());
        }

        /// <summary>
        /// Checks to see if a given collection is in this composite collection.
        /// </summary>
        /// <param name="containCollection">The collection to check.</param>
        /// <returns><c>true</c> if the collection contains the given collection; otherwise, <c>false</c>.</returns>
        public bool Contains(IEnumerable<TViewModel> containCollection)
        {
            if (containCollection == null)
                throw new ArgumentNullException(nameof(containCollection));

            bool result = false;

            Dispatcher.Dispatch(() =>
            {
                foreach (var existingCollection in this)
                {
                    if (existingCollection == containCollection)
                    {
                        result = true;
                        break;
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// Returns the index in this composite collection where the given collection is found.
        /// </summary>
        /// <param name="indexCollection">The collection to retrieve the index for.</param>
        /// <returns>If the collection appears in the composite collection, then the zero-based index in the composite collection where the given collection is found; otherwise, <c>-1</c>.</returns>
        public int IndexOf(IEnumerable<TViewModel> indexCollection)
        {
            if (indexCollection == null)
                throw new ArgumentNullException(nameof(indexCollection));

            int result = 0;

            Dispatcher.Dispatch(() =>
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i] == indexCollection)
                    {
                        result = i;
                        break;
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// Inserts a collection in the composite collection at a given index.
        /// All collections after the given position are moved down by one.
        /// </summary>
        /// <param name="insertIndex">The index to insert the collection at.</param>
        /// <param name="insertCollection">The collection reference to add to the composite collection.</param>
        public void Insert(int insertIndex, IEnumerable<TViewModel> insertCollection)
        {
            if (insertCollection == null)
                throw new ArgumentNullException(nameof(insertCollection));

            Dispatcher.Dispatch(() => base.Insert(insertIndex, new CollectionContainer { Collection = insertCollection }));
        }

        /// <summary>
        /// Removes the given collection reference from the composite collection.
        /// All remaining collections move up by one.
        /// </summary>
        /// <param name="removeCollection">The collection to remove.</param>
        public void Remove(IEnumerable<TViewModel> removeCollection)
        {
            if (removeCollection == null)
                throw new ArgumentNullException(nameof(removeCollection));

            Dispatcher.Dispatch(() =>
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i] == removeCollection)
                    {
                        base.RemoveAt(i);
                        break;
                    }
                }
            });
        }

        /// <summary>
        /// Removes a collection from the composite collection at the given index.
        /// All remaining collections move up by one.
        /// </summary>
        /// <param name="removeIndex">The index at which to remove a collection.</param>
        new public void RemoveAt(int removeIndex)
        {
            Dispatcher.Dispatch(() => base.RemoveAt(removeIndex));
        }

        #region Overshadowed members
        /// <summary>
        /// Overshadowed member of the base class <see cref="CompositeCollection"/>. Do not use this.
        /// </summary>
        [Obsolete("This member of the CompositeCollection base class cannot be used.", true)]
        new public int Add(object newItem)
        {
            throw new NotSupportedException("This member of the CompositeCollection base class cannot be used.");
        }

        /// <summary>
        /// Overshadowed member of the base class <see cref="CompositeCollection"/>. Do not use this.
        /// </summary>
        [Obsolete("This member of the CompositeCollection base class cannot be used.", true)]
        new public bool Contains(object containItem)
        {
            throw new NotSupportedException("This member of the CompositeCollection base class cannot be used.");
        }

        /// <summary>
        /// Overshadowed member of the base class <see cref="CompositeCollection"/>. Do not use this.
        /// </summary>
        [Obsolete("This member of the CompositeCollection base class cannot be used.", true)]
        new public int IndexOf(object indexItem)
        {
            throw new NotSupportedException("This member of the CompositeCollection base class cannot be used.");
        }

        /// <summary>
        /// Overshadowed member of the base class <see cref="CompositeCollection"/>. Do not use this.
        /// </summary>
        [Obsolete("This member of the CompositeCollection base class cannot be used.", true)]
        new public int Insert(int insertIndex, object insertItem)
        {
            throw new NotSupportedException("This member of the CompositeCollection base class cannot be used.");
        }

        /// <summary>
        /// Overshadowed member of the base class <see cref="CompositeCollection"/>. Do not use this.
        /// </summary>
        [Obsolete("This member of the CompositeCollection base class cannot be used.", true)]
        new public void Remove(object removeItem)
        {
            throw new NotSupportedException("This member of the CompositeCollection base class cannot be used.");
        }
        #endregion
    }
}
