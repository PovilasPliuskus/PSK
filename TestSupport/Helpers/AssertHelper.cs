using System.Reflection;

namespace TestSupport.Helpers
{
    public class AssertHelper
    {
        public static void AreEqualAllProperties(object expected, object actual)
        {
            PropertyInfo[] properties = expected.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var expectedValue = property.GetValue(expected);
                var actualValue = property.GetValue(actual);

                if (!object.Equals(expectedValue, actualValue))
                {
                    throw new Exception($"Property '{property.Name}' does not match. Expected: {expectedValue}, Actual: {actualValue}");
                }
            }
        }
    }
}
