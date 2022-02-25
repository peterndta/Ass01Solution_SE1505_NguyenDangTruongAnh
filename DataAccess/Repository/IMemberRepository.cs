using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMembers();
        MemberObject GetMemberById(int memberId);
        void InsertMember(MemberObject member);
        void DeleteMember(int memberId);
        void UpdateMember(MemberObject member);
        MemberObject Login(string email, string password);
        MemberObject SearchByID(int memberID);
        IEnumerable<MemberObject> SortingMember();
        IEnumerable<MemberObject> FilterByCountry(string country);

    }
}
