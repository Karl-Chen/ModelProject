using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.ViewModels;
using WebProject.WorkFunction;

namespace WebProject.Services
{
    public class MemberServices
    {
        private readonly GuestModelContext _context;
        private readonly MimaHandler _mimaHandler;
        public MemberServices(GuestModelContext context, MimaHandler mimaHandler)
        {
            _context = context;
            _mimaHandler = mimaHandler;
        }

        public async Task<MemberAcc> CheckMemberAcc(MemberAcc login)
        {
            string sql = "Select * from MemberAcc where Account = @Account and Mima = @Mima";
            string loginmima = _mimaHandler.Get_SHA256_Hash(login.Mima);
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Account", login.Account),
                    new SqlParameter("@Mima", loginmima)
                };
            return await _context.MemberAcc.FromSqlRaw(sql, parameters).FirstOrDefaultAsync();
        }

        public async Task<string> GetMemberIDByAccount(string account)
        {
            return await _context.MemberAcc.Where(c => c.Account == account).Select(c => c.MemberID).FirstOrDefaultAsync();
        }

        public async Task<List<Member>> GetMemberList()
        {
            return await _context.Member.ToListAsync();
        }

        public async Task<string> GetNameByMemberID(string memberID)
        {
            return await _context.Member.Where(c => c.MemberID == memberID).Select(c => c.Name).FirstOrDefaultAsync();
        }

        public async Task<string> GetNameByAccount(string account)
        {
            string memberID = await GetMemberIDByAccount(account);
            return await _context.Member.Where(c => c.MemberID == memberID).Select(c => c.Name).FirstOrDefaultAsync();
        }

        public async Task<VMMembers> GetVMMemberByAccount(string account)
        {
            MemberAcc memberAcc = await GetMemberAccByAccount(account);
            Member member = await GetMemberByMmeberID(memberAcc.MemberID);
            MemberTel memberTel = await GetMemberTelByMmeberID(member.MemberID);
            if (member == null || memberAcc == null || memberTel == null)
                return null;
            return MemberToVMMember(member, memberAcc, memberTel);
        }

        public async Task EditMemberGroup(VMMembers vMMembers, string account)
        {
            string memberID = "";
            memberID = await GetMemberIDByAccount(account);
            if (memberID == "")
                return;
            MemberAcc memberAcc = VMMMemberToMemberAcc(vMMembers, memberID);
            Member member = VMMMemberToMember(vMMembers, memberID);
            MemberTel memberTel = VMMMemberToMemberTel(vMMembers, memberID);
            SaveMmeberGroup(member, memberAcc, memberTel);
        }

        public async Task<Member> GetMemberbyAcc(string acc)
        {
            string memberID = await GetMemberIDByAccount(acc);
            if (memberID == "")
                return null;
            return await GetMemberByMmeberID(memberID);
        }

        private async Task<MemberAcc> GetMemberAccByAccount(string account)
        {
            return await _context.MemberAcc.FirstOrDefaultAsync(m => m.Account == account);
        }

        private async Task<Member> GetMemberByMmeberID(string memberID)
        {
            return await _context.Member.FirstOrDefaultAsync(m => m.MemberID == memberID);
        }

        private async Task<MemberTel> GetMemberTelByMmeberID(string memberID)
        {
            return await _context.MemberTel.FirstOrDefaultAsync(m => m.MemberID == memberID);
        }
        public async Task<string> CreateNewMemberID()
        {
            string mid = "M00001";
            var memberID = await _context.Member.OrderByDescending(c => c.MemberID).Select(c => c.MemberID).FirstOrDefaultAsync();
            if (memberID != null && memberID.Length > 0)
            {
                mid = memberID.Substring(1, memberID.Length - 1);
                int intMid = int.Parse(mid);
                intMid++;
                mid = "M" + intMid.ToString().PadLeft(5, '0');
            }
            return mid;
        }

        public async Task SaveMmeberGroup(Member member, MemberAcc memberAcc, MemberTel memberTel)
        {
            _context.Add(member);
            await _context.SaveChangesAsync();
            _context.Add(memberAcc);
            await _context.SaveChangesAsync();
            _context.Add(memberTel);
            await _context.SaveChangesAsync();
        }


        public bool VMMembersExists(string id)
        {
            return _context.MemberAcc.Any(e => e.Account == id);
        }

        public Member VMMMemberToMember(VMMembers vMMembers, string memberID)
        {
            Member member = new Member();
            member.MemberID = memberID;
            member.Address = vMMembers.Address;
            member.Name = vMMembers.Name;
            member.Email = vMMembers.Email;
            return member;
        }
        public MemberAcc VMMMemberToMemberAcc(VMMembers vMMembers, string memberID)
        {
            MemberAcc memberAcc = new MemberAcc();
            memberAcc.Account = vMMembers.Account;
            memberAcc.Mima = _mimaHandler.Get_SHA256_Hash(vMMembers.Mima);
            memberAcc.MemberID = memberID;

            return memberAcc;
        }

        public MemberTel VMMMemberToMemberTel(VMMembers vMMembers, string memberID)
        {
            MemberTel memberTel = new MemberTel();
            memberTel.TelNumber = vMMembers.TelNumber;
            memberTel.MemberID = memberID;

            return memberTel;
        }

        private VMMembers MemberToVMMember(Member member, MemberAcc memberacc, MemberTel membertel)
        {
            VMMembers vMMembers = new VMMembers();
            vMMembers.Mima = memberacc.Mima;
            vMMembers.Account = memberacc.Account;
            vMMembers.Address = member.Address;
            vMMembers.TelNumber = membertel.TelNumber;
            vMMembers.Email = member.Email;
            vMMembers.Name = member.Name;
            return vMMembers;
        }
    }
}
