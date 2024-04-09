namespace Intex_Group3_6.Models;

public class EFDataRepo : IDataRepo
{
    private DataContext _context;
    public EFDataRepo(DataContext temp) 
    { 
        _context = temp;
    }

}