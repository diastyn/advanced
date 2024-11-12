namespace Advanced.Application.Requests;

public class CreateProductRequest
{
    private string _name;
    private string _description;
    private string _category;

    public CreateProductRequest(string name, string description, string category, double price)
    {
        _name = name;
        _description = description;
        _category = category;
        Price = price;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Name can not be null or empty");
            }

            _name = value;
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Description can not be null or empty");
            }

            _description = value;
        }
    }

    public string Category
    {
        get => _category;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Category can not be null or empty");
            }

            _category = value;
        }
    }
    
    public double Price { get; set; }
}