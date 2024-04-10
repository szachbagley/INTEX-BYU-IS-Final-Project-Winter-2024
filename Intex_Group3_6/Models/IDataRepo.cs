namespace Intex_Group3_6.Models;

public interface IDataRepo
{
    public IQueryable<Product> Products { get; }
}