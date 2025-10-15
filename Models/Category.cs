namespace frist_project_one.Models
{
    public class Category
    {
    public Guid CategoryID { get; set; }
    public string Name { get; set; }= string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CategoryAt { get; set; }
    
    
    public Category()
        {
            CategoryID = Guid.NewGuid();
            CategoryAt = DateTime.UtcNow;     
        }
    }
}