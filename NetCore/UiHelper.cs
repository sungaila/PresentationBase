using System.Windows;
using System.Windows.Media;

namespace PresentationBase
{
    /// <summary>
    /// Provides functions for traversing and searching the visual tree.
    /// </summary>
    public static class UiHelper
    {
        /// <summary>
        /// Tries to find a child in the visual tree with the given type <typeparamref name="T"/> and <paramref name="childName"/>.
        /// </summary>
        /// <typeparam name="T">The type of the child to find</typeparam>
        /// <param name="parent">The parent containing the child.</param>
        /// <param name="childName">The <see cref="FrameworkElement.Name"/> of the child to find.</param>
        public static T? TryFindChild<T>(this DependencyObject parent, string? childName = null)
            where T : DependencyObject
        {
            if (parent == null)
                return null;

            T? foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (!(child is T t))
                {
                    foundChild = TryFindChild<T>(child, childName);

                    if (foundChild != null)
                        break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    if (child is FrameworkElement frameworkElement && frameworkElement.Name == childName)
                    {
                        foundChild = t;
                        break;
                    }
                }
                else
                {
                    foundChild = t;
                    break;
                }
            }

            return foundChild;
        }
    }
}
