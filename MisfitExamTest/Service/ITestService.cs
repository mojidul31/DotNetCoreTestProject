using MisfitExamTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisfitExamTest.Service
{
    public interface ITestService
    {
        List<ShowInformationVm> GetAlInformations();
        int AddUserInfo(UserInfo userInfo);
        int AddInformation(Information information);
        int GetUserIdByName(string userName);
    }
}
