using MisfitExamTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisfitExamTest.Repo
{
    public interface ITestRepository
    {
        List<ShowInformationVm> GetAlInformations();
        int AddUserInfo(UserInfo userInfo);
        int AddInformation(Information information);
        int GetUserIdByName(string userName);
    }
}
