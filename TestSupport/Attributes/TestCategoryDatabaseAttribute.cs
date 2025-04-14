using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSupport.Attributes;

public class TestCategoryDatabaseAttribute : TestCategoryBaseAttribute
{
    public override IList<string> TestCategories
    {
        get { return ["Database"]; }
    }
}
