using MisfitExamTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisfitExamTest.Repo
{
    public class TestRepositoryImpl: ITestRepository
    {
        private readonly DatabaseContext _context;
        private bool _disposed = false;

        public TestRepositoryImpl(DatabaseContext context)
        {
            _context = context;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public List<ShowInformationVm> GetAlInformations()
        {
            try
            {
                //var allInformation =  _context.Information.ToList();
                var allInformation = (from c in _context.Information
                                      join o in _context.UserInfo
                                      on c.UserRefId equals o.UserId
                                      select new
                                      {
                                          c.InfoId,
                                          c.CreateDate,
                                          c.FirstNo,
                                          c.SecondNo,
                                          c.SumOfTwo,
                                          o.UserName
                                      });

                List<ShowInformationVm> informations = new List<ShowInformationVm>();
                ShowInformationVm information = null;                
                foreach (var item in allInformation)
                {
                    information = new ShowInformationVm();
                    information.InfoId = item.InfoId;
                    information.UserName = item.UserName;
                    information.FirstNo = item.FirstNo;
                    information.SecondNo = item.SecondNo;
                    information.SumOfTwo = item.SumOfTwo;
                    information.CreateDate = item.CreateDate?.ToString("MM/dd/2019");
                    informations.Add(information);
                }
                return informations;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AddUserInfo(UserInfo userInfo)
        {
            try
            {
                int result = -1;

                if (userInfo != null)
                {
                    userInfo.UserId = 0;                    
                    _context.UserInfo.Add(userInfo);
                    _context.SaveChanges();
                    result = userInfo.UserId;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AddInformation(Information information)
        {
            try
            {
                int result = -1;

                if (information != null)
                {
                    information.InfoId = 0;
                    information.CreateDate = DateTime.Now;
                    _context.Information.Add(information);
                    _context.SaveChanges();
                    result = information.InfoId;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetUserIdByName(string userName)
        {
            try
            {
                var user =  _context.UserInfo.FirstOrDefault(m=>m.UserName == userName);
                if(user != null && user.UserId > 0)
                {
                    return user.UserId;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
