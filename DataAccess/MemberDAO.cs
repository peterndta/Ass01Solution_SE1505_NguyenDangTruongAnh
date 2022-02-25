using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DataAccess
{
    public class MemberDAO
    {
        //private static List<MemberObject> AccountList = new List<MemberObject>
        //{
        //   new MemberObject{ MemberID = 1, MemberName = "FStore", Email= "admin@fstore.com",
        //   Password = "admin@@", City = "HCM", Country = "VietNam" },
        //};

        //Initialize member list
        private static List<MemberObject> MemberList = new List<MemberObject>
        {
            new MemberObject{ MemberID = 1, MemberName = "Truong Anh", Email= "asd@gmail.com",
            Password = "123", City = "HCM", Country = "Vietnam" },
            new MemberObject{ MemberID = 2, MemberName = "Peter", Email= "bcd@gmail.com",
            Password = "123", City = "Hanoi", Country = "Vietnam" }
        };
        //Using Singleton Pattern
        private static MemberDAO instance = null;
        private static readonly object instnaceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instnaceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------------------
        public List<MemberObject> GetMemberList => MemberList;

        //-------------------------------------------------
        public MemberObject GetMemberByID(int memberID)
        {
            //using LINQ to Object
            MemberObject member = MemberList.SingleOrDefault(pro => pro.MemberID == memberID);
            return member;
        }
        //-------------------------------------------------
        //Add new a member
        public void AddNew(MemberObject member)
        {
            MemberObject pro = GetMemberByID(member.MemberID);
            if (pro == null)
            {
                MemberList.Add(member);
            }
            else
            {
                throw new Exception("Member is already exists.");
            }
        }
        //-------------------------------------------------
        //Update a member
        public void Update(MemberObject member)
        {
            MemberObject c = GetMemberByID(member.MemberID);
            if (c != null)
            {
                var index = MemberList.IndexOf(c);
                MemberList[index] = member;
            }
            else
            {
                throw new Exception("Member does not already exist.");
            }
        }
        //--------------------------------------------------
        //Remove a member
        public void Remove(int MemberID)
        {
            MemberObject p = GetMemberByID(MemberID);
            if (p != null)
            {
                MemberList.Remove(p);
            }
            else
            {
                throw new Exception("Member does not already exists.");
            }
        }//end Remove

        //---------------------------------------------------
        //Login
        public MemberObject Login(string email, string password)
        {
            //using LINQ to Object
            MemberObject account = MemberList.SingleOrDefault(pro => pro.Email == email && pro.Password == password);
            return account;
        }
        //---------------------------------------------------
        //Search
        public MemberObject SearchByID(int ID)
        {
            //using LINQ to Object
            MemberObject member = MemberList.SingleOrDefault(pro => pro.MemberID == ID);
            return member;
        }
        //---------------------------------------------------
        //Sort
        public List<MemberObject> SortingMember() 
        {
            List<MemberObject> memberSort = MemberList;
            memberSort.Reverse();
            return memberSort;
        }

    }//end Class
}

