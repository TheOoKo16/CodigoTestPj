using Core.Interface;
using Data.Models;
using Infra.Repository;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Infra.UnitOfWork
{
    public class UnitOfWork
    {

      
        private IRepository<evoucher> _evoucherRepo;
        private IRepository<customer> _customerRepo;


        private IRepository<purchase> _purchaseRepo;

        private ApplicationDbContext _ctx;
        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IRepository<evoucher> evoucherRepo
        {
            get
            {
                if (_evoucherRepo == null)
                {
                    _evoucherRepo = new Repository<evoucher>(_ctx);
                }
                return _evoucherRepo;
            }
        }
        public IRepository<customer> customerRepo
        {
            get
            {
                if (_customerRepo == null)
                {
                    _customerRepo = new Repository<customer>(_ctx);
                }
                return _customerRepo;
            }
        }
        public IRepository<purchase> purchaseRepo
        {
            get
            {
                if (_purchaseRepo == null)
                {
                    _purchaseRepo = new Repository<purchase>(_ctx);
                }
                return _purchaseRepo;
            }
        }




    }
}
