using IRS4TT.Domains;

namespace IRS4TT.Interfaces
{
    public interface ICoverGroupService
    {
        IEnumerable<CoverGroup> GetAll();
        CoverGroup GetById(int id);
        void Add(CoverGroup group);
        void Update(CoverGroup group);
        void Delete(int id);
    }


}
