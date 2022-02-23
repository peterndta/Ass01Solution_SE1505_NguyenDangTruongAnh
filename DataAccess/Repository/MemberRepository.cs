using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        
        IEnumerable<MemberObject> IMemberRepository.GetMembers() => MemberDAO.Instance.GetMemberList;

        MemberObject IMemberRepository.GetMemberById(int memberId) => MemberDAO.Instance.GetMemberByID(memberId);

        void IMemberRepository.InsertMember(MemberObject member)
        {
            MemberDAO.Instance.AddNew(member);
        }

        void IMemberRepository.DeleteMember(int memberId)
        {
            MemberDAO.Instance.Remove(memberId);
        }

        void IMemberRepository.UpdateMember(MemberObject member)
        {
            MemberDAO.Instance.Update(member);
        }

        MemberObject IMemberRepository.Login(string email, string password) => MemberDAO.Instance.Login(email, password);
        
    }
}
