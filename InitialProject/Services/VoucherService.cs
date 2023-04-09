namespace InitialProject.Services
{
    using InitialProject.Model;
    using InitialProject.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VoucherService
    {
        private readonly VoucherRepository _repository;
        private List<Voucher> _vouchers;
        public VoucherService() 
        { 
            _repository = new VoucherRepository();
            _vouchers = _repository.GetAll();
        }

        public List<Voucher> GetAllForUser(int userId)
        {
            return _vouchers.Where(v => v.UserId == userId).ToList();
        }

        public void Delete(Voucher voucher)
        {
            _repository.Delete(voucher);
        } 

    }
}
