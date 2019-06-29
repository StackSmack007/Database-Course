
public class Rootobject
{
    public Categories Categories { get; set; }
}

public class Categories
{
    public Category[] Category { get; set; }
}

public class Category
{
    public string name { get; set; }
    public string count { get; set; }
    public string averagePrice { get; set; }
    public string totalRevenue { get; set; }
}
