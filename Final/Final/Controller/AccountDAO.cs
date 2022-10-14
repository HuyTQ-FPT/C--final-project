using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.models;

namespace Final.Controller
{
    internal class AccountDAO
    {

        internal List<string> getAllUserName()
        {
            using prnSQLContext context = new prnSQLContext();
            return context.Accounts.Select(x => x.Username).ToList();
        }
        public Account acc(String username,String password)
        {
            try
            {
                using prnSQLContext context = new prnSQLContext();
                return context.Accounts.Where(x => x.Username == username && x.Password == password)
                    .ToList()
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ChangePas(String username,String pass)
        {
            using prnSQLContext context = new prnSQLContext();
            try
            {
                Account acc = context.Accounts.Where(x => x.Username == username)
                   .ToList()
                   .FirstOrDefault();
                if (acc != null)
                {
                    acc.Password = pass;
                    context.Entry<Account>(acc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void ChangeInfo(String username, String name, String phone)
        {
            using prnSQLContext context = new prnSQLContext();
            try
            {
                Account acc = context.Accounts.Where(x => x.Username == username)
                   .ToList()
                   .FirstOrDefault();
                if (acc != null)
                {
                    acc.Name = name;
                    acc.Phone = phone;
                    context.Entry<Account>(acc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}
