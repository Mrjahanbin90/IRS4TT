using IRS4TT.Data;
using IRS4TT.Domains;
using IRS4TT.Interfaces;

public class CoverGroupService : ICoverGroupService
{
    private readonly AppDbContext _context;

    public CoverGroupService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<CoverGroup> GetAll() => _context.CoverGroups.ToList();

    public CoverGroup GetById(int id) => _context.CoverGroups.Find(id);

    public void Add(CoverGroup group)
    {
        _context.CoverGroups.Add(group);
        _context.SaveChanges();
    }

    public void Update(CoverGroup group)
    {
        _context.CoverGroups.Update(group);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var group = _context.CoverGroups.Find(id);
        if (group != null)
        {
            _context.CoverGroups.Remove(group);
            _context.SaveChanges();
        }
    }
}
