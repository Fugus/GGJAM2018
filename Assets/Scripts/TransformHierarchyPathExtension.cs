using UnityEngine;


public static class TransformHierarchyPathExtension
{
    public static string GetHierarchyPath(this Transform transform)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendFormat("/{0}", transform.name);
        while (transform.parent != null)
        {
            transform = transform.parent;
            sb.Insert(0, transform.name);
            sb.Insert(0, "/");
        }
        return sb.ToString();
    }
}
