using IRS4TT.Data;
using IRS4TT.Domains;
using IRS4TT.Interfaces;

public class CoverService : ICoverService
{
    private readonly AppDbContext _context;

    public CoverService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Cover> GetAll() => _context.Covers.ToList();

    public Cover GetById(int id) => _context.Covers.Find(id);

    public void Add(Cover cover)
    {
        _context.Covers.Add(cover);
        _context.SaveChanges();
    }

    public void Update(Cover cover)
    {
        _context.Covers.Update(cover);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var cover = _context.Covers.Find(id);
        if (cover != null)
        {
            _context.Covers.Remove(cover);
            _context.SaveChanges();
        }
    }
}
