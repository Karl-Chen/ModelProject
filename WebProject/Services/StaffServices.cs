using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.WorkFunction;

namespace WebProject.Services
{
    public class StaffServices
    {
        private readonly GuestModelContext _guestModelContext;
        public StaffServices(GuestModelContext guestModelContext)
        {
            _guestModelContext = guestModelContext;
        }

        public async Task<StaffAcc> CheckMemberAcc(StaffAcc login)
        {
            string sql = "Select * from StaffAcc where Account = @Account and Mima = @Mima";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Account", login.Account),
                new SqlParameter("@Mima", login.Mima)
                };
            return await _guestModelContext.StaffAcc.FromSqlRaw(sql, parameters).FirstOrDefaultAsync();
        }

        public async Task<string> GetStaffIDByAccount(string account)
        {
            return await _guestModelContext.StaffAcc.Where(c => c.Account == account).Select(c => c.StaffID).FirstOrDefaultAsync();
        }

        public async Task<string> GetNameByAccount(string account)
        {
            string StaffID = await GetStaffIDByAccount(account);
            return await _guestModelContext.Staff.Where(c => c.StaffID == StaffID).Select(c => c.Name).FirstOrDefaultAsync();
        }
    }
}
