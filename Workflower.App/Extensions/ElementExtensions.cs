namespace Workflower.App.Extensions;

internal static class ElementExtensions
{
    public static T? FindAncestor<T>(this Element element) where T : Element
    {
        if (element.Parent is T ofType)
        {
            return ofType;
        }

        if (element.Parent != null)
        {
            return element.Parent.FindAncestor<T>();
        }

        return null;
    }
}