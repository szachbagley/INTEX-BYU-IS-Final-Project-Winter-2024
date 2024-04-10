namespace Intex_Group3_6.Models;

public class EFDataRepo : IDataRepo
{
    private DataContext _context;
    public EFDataRepo(DataContext temp) 
    { 
        _context = temp;
    }
    
    public void AddUser(User user) // Use this to add a user to the database
    {
        _context.Users.Add(user);
    }
    
    public void SaveChanges() // Use this to save any changes to the database
    {
        _context.SaveChanges();
    }

}