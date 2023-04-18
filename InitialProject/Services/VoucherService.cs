namespace InitialProject.Services
{
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using System.Collections.Generic;
    using System.Linq;

    public class VoucherService
    {
        private readonly VoucherRepository _repository;
        public VoucherService()
        {
            _repository = new VoucherRepository();
        }

        public List<Voucher> GetAllForUser(int userId)
        {
            return _repository.GetAll().Where(v => v.UserId == userId).ToList();
        }

        public void Delete(Voucher voucher)
        {
            _repository.Delete(voucher);
        }


    }
}
