using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Plugins;
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
        // 目前改變做法，把修改密碼跟修改會員資料拆開，不然太難做了，因為我密碼有加密，各種奇琶的原因啊....
        //public async Task EditMemberGroup(VMMembers vMMembers, string account)
        //{
        //    string memberID = "";
        //    memberID = await GetMemberIDByAccount(account);
        //    if (memberID == "")
        //        return;
        //    MemberAcc memberAcc = VMMMemberToMemberAcc(vMMembers, memberID);
        //    Member member = VMMMemberToMember(vMMembers, memberID);
        //    MemberTel memberTel = VMMMemberToMemberTel(vMMembers, memberID);
        //    SaveMmeberGroup(member, memberAcc, memberTel);
        //}

        public async Task EditMemberAcc(VMEditMemberAcc vMMembers)
        {
            if (vMMembers.MemberID == "")
                return;
            var memberAcc = await _context.MemberAcc.FindAsync(vMMembers.Account);
            if (memberAcc == null) return;
            memberAcc.Mima = _mimaHandler.Get_SHA256_Hash(vMMembers.Mima);
            _context.Update(memberAcc);
            await _context.SaveChangesAsync();
        }

        public async Task EditMember(VMEditMember vmEditMember)
        {
            if (vmEditMember.MemberID == "")
                return;
            var newMember = await _context.Member.FindAsync(vmEditMember.MemberID);
            if (newMember == null) return;
            newMember.Email = vmEditMember.Email;
            newMember.Address = vmEditMember.Address;
            _context.Update(newMember);
            await _context.SaveChangesAsync();

            var newMemberTel = await _context.MemberTel.FirstOrDefaultAsync(c => c.MemberID == newMember.MemberID);
            if (newMemberTel == null) return;
            newMemberTel.TelNumber = vmEditMember.TelNumber;
            await _context.SaveChangesAsync();
        }

        public async Task<Member> GetMemberbyAcc(string acc)
        {
            string memberID = await GetMemberIDByAccount(acc);
            if (memberID == "")
                return null;
            return await GetMemberByMmeberID(memberID);
        }

        public async Task<VMEditMemberAcc> GetVMEditMemberAccByAcc(string account)
        {
            var memberAcc = await GetMemberAccByAccount(account);
            VMEditMemberAcc vMEditMemberAcc = new VMEditMemberAcc();
            vMEditMemberAcc.Account = account;
            vMEditMemberAcc.MemberID = memberAcc.MemberID;
            return vMEditMemberAcc;
        }

        public async Task<VMEditMember> GetVMEditMemberByAcc(string account)
        {
            var member = await GetMemberbyAcc(account);
            var memberTel = await GetMemberTelByMmeberID(member.MemberID);
            VMEditMember vMEditMember = new VMEditMember();
            vMEditMember.MemberID = member.MemberID;
            vMEditMember.Address = member.Address;
            vMEditMember.Email = member.Email;
            vMEditMember.Name = member.Name;
            vMEditMember.TelNumber = memberTel.TelNumber;
            return vMEditMember;
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
            string mid = "M0000001";
            var memberID = await _context.Member.OrderByDescending(c => c.MemberID).Select(c => c.MemberID).FirstOrDefaultAsync();
            if (memberID != null && memberID.Length > 0)
            {
                mid = memberID.Substring(1, memberID.Length - 1);
                int intMid = int.Parse(mid);
                intMid++;
                mid = "M" + intMid.ToString().PadLeft(7, '0');
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


        public async Task<bool> VMMembersExists(string id)
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

        public async Task<bool> isOldMima(VMEditMemberAcc vmMember)
        {
            string ret = "";
            if (vmMember.OldMima != null && vmMember.OldMima != "")
            {
                MemberAcc oldAcc = await GetMemberAccByAccount(vmMember.Account);
                string loginmima = _mimaHandler.Get_SHA256_Hash(vmMember.OldMima);
                if (oldAcc != null && oldAcc.Mima == loginmima)
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        //public async Task<VMMembers> 

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

        private async Task<string> SyncMima(VMMembers vmMember)
        {
            string ret = "";
            if (vmMember.OldMima != null && vmMember.OldMima != "")
            {
                MemberAcc oldAcc = await GetMemberAccByAccount(vmMember.Account);
                MemberAcc oldAcc2 = await CheckMemberAcc(oldAcc);
                if (oldAcc2 != null)
                {

                }

            }
            return ret; 
        }
    }
}
