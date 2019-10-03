using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MisfitExamTest.Models;
using MisfitExamTest.Repo;

namespace MisfitExamTest.Service
{
    public class TestServiceImpl : ITestService
    {
        private readonly ITestRepository _testRepository;
        public TestServiceImpl(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        public int AddInformation(Information information)
        {
            try
            {
                return _testRepository.AddInformation(information);
            }
            catch
            {
                throw;
            }
        }

        public int AddUserInfo(UserInfo userInfo)
        {
            try
            {
                return _testRepository.AddUserInfo(userInfo);
            }
            catch
            {
                throw;
            }
        }

        public List<ShowInformationVm> GetAlInformations()
        {
            try
            {
                return _testRepository.GetAlInformations();
            }
            catch
            {
                throw;
            }
        }

        public int GetUserIdByName(string userName)
        {
            try
            {
                return _testRepository.GetUserIdByName(userName);
            }
            catch
            {
                throw;
            }
        }
    }
}
