using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.IRepository
{
    public interface IVoucherRepository
    {
        void CheckValidity();
        void Delete(Voucher voucher);
        List<Voucher> GetAll();
        int NextId();
        Voucher Save(Voucher voucher);
    }
}