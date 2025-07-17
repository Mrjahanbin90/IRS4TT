using IRS4TT.Domains;

namespace IRS4TT.Interfaces
{
    public interface ICoverService
    {
        IEnumerable<Cover> GetAll();
        Cover GetById(int id);
        void Add(Cover cover);
        void Update(Cover cover);
        void Delete(int id);
    }


}
