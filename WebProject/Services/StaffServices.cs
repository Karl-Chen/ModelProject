using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
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

        public async Task<bool> CheckTheSameAcc(string acc)
        {
            string StaffID = await GetStaffIDByAccount(acc);
            if (StaffID == null)
                return true;
            return false;
        }

        public async Task<string> HandleStaffID(Staff staffAcc)
        {
            DateTime date = DateTime.Today;
            int year = date.Year - 1911;
            var staffID = await _guestModelContext.Staff.OrderByDescending(c => c.StaffID).Select(c => c.StaffID).FirstOrDefaultAsync();
            if (staffID != null && staffID.Length > 0)
            {
                int staffYear = int.Parse(staffID.Substring(0, 3));
                if (year > staffYear)
                {
                    staffID = year.ToString() + "001";
                }
                else
                {
                    staffID = staffID.Substring(3, 3);
                    int intMid = int.Parse(staffID);
                    intMid++;
                    staffID = year.ToString() + intMid.ToString().PadLeft(3, '0');
                }
            }
            return staffID;
        }

        public async Task SaveStaff(Staff staff)
        {
            _guestModelContext.Add(staff);
            await _guestModelContext.SaveChangesAsync();
        }

        public async Task SaveStaffAcc(StaffAcc staffAcc)
        {
            _guestModelContext.Add(staffAcc);
            await _guestModelContext.SaveChangesAsync();
        }
    }
}
