using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Services
{
    public interface IDonhangChitietSvc
    {
        int AddDonhangChitietSvc(DonhangChitiet donhangChitiet);
    }
    public class DonhangChitietSvc : IDonhangChitietSvc
    {
        protected ASMContext _context;
        public DonhangChitietSvc(ASMContext context)
        {
            _context = context;
        }
        public int AddDonhangChitietSvc(DonhangChitiet donhangChitiet)
        {
            int ret = 0;
            try
            {
                _context.Add(donhangChitiet);
                _context.SaveChanges();
                ret = donhangChitiet.ChitietID;

            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
    }
}
