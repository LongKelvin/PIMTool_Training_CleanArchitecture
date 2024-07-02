using System.Collections;
using System.Text;

namespace ProjectManagement.Common.Extensions
{
    public static class ObjectExtension
    {
        public static string DisplayPropertyValues(this object obj, int indentLevel = 0)
        {
            if (obj == null)
            {
                return "null";
            }

            StringBuilder sb = new();
            Type type = obj.GetType();

            // Handle primitive types, strings, and enums directly
            if (type.IsPrimitive || type == typeof(string) || type.IsEnum)
            {
                return obj!.ToString() ?? string.Empty;
            }

            // Use recursion for nested objects
            sb.Append(new string(' ', indentLevel * 4)); // Add indentation
            sb.AppendLine("{");

            bool firstProperty = true;
            foreach (var property in type.GetProperties())
            {
                if (!firstProperty)
                {
                    sb.AppendLine(","); // Add a comma and line break before each new property except the first
                }

                sb.Append(new string(' ', (indentLevel + 1) * 4)); // Add indentation
                sb.Append($"{property.Name}: ");
                object? propertyValue = property.GetValue(obj);
                if (propertyValue != null)
                {
                    // Handle nested collections separately
                    if (propertyValue is IEnumerable enumerable && propertyValue is not string)
                    {
                        sb.AppendLine();
                        sb.Append(Enumerable.Cast<object>(enumerable).ToPropertyValuesString(indentLevel + 2));
                    }
                    else
                    {
                        sb.Append(propertyValue.DisplayPropertyValues(indentLevel + 1));
                    }
                }
                else
                {
                    sb.Append("null");
                }

                firstProperty = false;
            }
            sb.AppendLine();
            sb.Append(new string(' ', indentLevel * 4)); // Add indentation
            sb.Append('}');
            return sb.ToString();
        }

        private static string ToPropertyValuesString(this IEnumerable<object> collection, int indentLevel = 0)
        {
            if (collection == null)
            {
                return "null";
            }

            StringBuilder sb = new(new string(' ', indentLevel * 4)); // Add indentation
            sb.AppendLine("[");
            bool firstItem = true;
            foreach (var item in collection)
            {
                if (!firstItem)
                {
                    sb.AppendLine(","); // Add a comma and line break before each new item except the first
                }

                sb.Append(new string(' ', (indentLevel + 1) * 4)); // Add indentation
                sb.Append(item.DisplayPropertyValues(indentLevel + 1));
                firstItem = false;
            }
            sb.AppendLine();
            sb.Append(new string(' ', indentLevel * 4)); // Add indentation
            sb.Append(']');
            return sb.ToString();
        }

        public static string ToPropertyValuesOnlyString(this object obj)
        {
            if (obj == null)
            {
                return "null";
            }

            StringBuilder sb = new();
            Type type = obj.GetType();

            // Handle primitive types and strings directly
            if (type.IsPrimitive || type == typeof(string))
            {
                return obj!.ToString() ?? string.Empty;
            }

            // Use recursion for nested objects

            bool firstProperty = true;
            foreach (var property in type.GetProperties())
            {
                if (!firstProperty)
                {
                    sb.Append(", ");
                }
                firstProperty = false;

                object? propertyValue = property.GetValue(obj);
                if (propertyValue != null)
                {
                    // Handle nested collections separately
                    if (propertyValue is IEnumerable enumerable && propertyValue is not string)
                    {
                        sb.Append(Enumerable.Cast<object>(enumerable).ToPropertyValuesString());
                    }
                    else
                    {
                        sb.Append(propertyValue.DisplayPropertyValues());
                    }
                }
                else
                {
                    sb.Append("null");
                }
            }

            return sb.ToString();
        }
    }
}