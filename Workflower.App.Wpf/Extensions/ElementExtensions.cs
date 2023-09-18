using System.Windows;

namespace Workflower.App.Wpf.Extensions;

internal static class ElementExtensions
{
    public static T? FindAncestor<T>(this DependencyObject @object) where T : FrameworkElement
    {
        if (@object is FrameworkElement element)
        {
            return element.FindAncestor<T>();
        }

        return null;
    }

    public static T? FindAncestor<T>(this FrameworkElement element) where T : FrameworkElement
    {
        if (element.Parent is T ofType)
        {
            return ofType;
        }

        if (element.Parent is FrameworkElement parent)
        {
            return parent.FindAncestor<T>();
        }

        return null;
    }
}