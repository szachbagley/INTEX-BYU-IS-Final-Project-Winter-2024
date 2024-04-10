namespace Intex_Group3_6.Models;

public interface IDataRepo
{
    void AddUser(User user);
    void SaveChanges();
}