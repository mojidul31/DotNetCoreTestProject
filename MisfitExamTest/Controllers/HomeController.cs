using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MisfitExamTest.Models;
using MisfitExamTest.Service;

namespace MisfitExamTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestService _testService;
        public HomeController(ITestService testService)
        {
            _testService = testService;
        }
        public IActionResult Index()
        {
            var res = _testService.GetAlInformations();
            return View();
        }

        public IActionResult About()
        {
            //var res = _testService.GetAlInformations();
            //ViewBag.Infos = res;
            InformationVm information = new InformationVm();
            return View(information);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstNo,SecondNo,UserName")] InformationVm informationVm)
        {
            if (ModelState.IsValid)
            {
                Information information = new Information();
                information.FirstNo = informationVm.FirstNo;
                information.SecondNo = informationVm.SecondNo;
                information.SumOfTwo = informationVm.FirstNo + informationVm.SecondNo;
                if (informationVm != null && !string.IsNullOrEmpty(informationVm.UserName))
                {
                    int userId = _testService.GetUserIdByName(informationVm.UserName);
                    if (userId > 0)
                    {
                        information.UserRefId = userId;
                    }
                    else
                    {
                        UserInfo userInfo = new UserInfo();
                        userInfo.UserName = informationVm.UserName;
                        int id = _testService.AddUserInfo(userInfo);
                        if(id > 0)
                        {
                            information.UserRefId = id;
                        }
                    }
                    var add = _testService.AddInformation(information);
                }

                return RedirectToAction(nameof(About));
            }
            return View(informationVm);
        }

        public IActionResult GetAll()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10, 20, 50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                // getting all Customer data  
                //var customerData = (from tempcustomer in _context.CustomerTB
                //select tempcustomer);
                var res = _testService.GetAlInformations();
                var customerData = _testService.GetAlInformations().AsEnumerable();

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    //customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                    if (String.IsNullOrEmpty(sortColumnDirection) || sortColumnDirection.ToUpper() == "ASC")
                    {
                        switch (sortColumn)
                        {
                            case "InfoId":
                                customerData = customerData.OrderBy(x => x.InfoId); break;
                            case "CreateDate":
                                customerData = customerData.OrderBy(x => x.CreateDate); break;
                            case "FirstNo":
                                customerData = customerData.OrderBy(x => x.FirstNo); break;
                            case "SecondNo":
                                customerData = customerData.OrderBy(x => x.SecondNo); break;
                            case "SumOfTwo":
                                customerData = customerData.OrderBy(x => x.SumOfTwo); break;
                            case "UserName":
                                customerData = customerData.OrderBy(x => x.UserName); break;
                                //...
                        }
                    }
                    else
                    {
                        switch (sortColumn)
                        {
                            case "InfoId":
                                customerData = customerData.OrderByDescending(x => x.InfoId); break;
                            case "CreateDate":
                                customerData = customerData.OrderByDescending(x => x.CreateDate); break;
                            case "FirstNo":
                                customerData = customerData.OrderByDescending(x => x.FirstNo); break;
                            case "SecondNo":
                                customerData = customerData.OrderByDescending(x => x.SecondNo); break;
                            case "SumOfTwo":
                                customerData = customerData.OrderByDescending(x => x.SumOfTwo); break;
                            case "UserName":
                                customerData = customerData.OrderByDescending(x => x.UserName); break;
                                //...
                        }
                    }
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.UserName == searchValue);
                }

                //total number of rows counts   
                recordsTotal = customerData.Count();
                //Paging   
                //var data = customerData.Skip(skip).Take(pageSize).ToList();
                //List<ShowInformationVm> target = data.AsEnumerable().Select(row => new ShowInformationVm {InfoId = row.InfoId<int>(0).GetValueOrDefault()}).ToList();
                var data = customerData.Select(x => new { x.InfoId, x.FirstNo, x.SecondNo, x.SumOfTwo, x.UserName}).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
            //return Json(_testService.GetAlInformations());
        }

        [HttpPost]        
        public IActionResult Searching(string userName)
        {
            List<ShowInformationVm> list = null;
            var res = _testService.GetAlInformations();
            if(res.Count > 0)
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    list = new List<ShowInformationVm>();
                    foreach (var item in res)
                    {
                        var exist = item.UserName.StartsWith(userName);
                        if (exist)
                        {
                            list.Add(item);
                        }
                    }
                }
                else
                {
                    list = res;
                }                
            }

            return Json(new { list });
        }

        [HttpPost]
        public IActionResult SearchingWithDate(string fromDate,string toDate)
        {
            List<ShowInformationVm> list = null;
            DateTime dt1 = DateTime.MinValue;
            
            var res = _testService.GetAlInformations();
            string f1 = GetModifiedDate(fromDate);
            string f2 = GetModifiedDate(toDate);

            DateTime StartDate =
                    Convert.ToDateTime(DateTime.ParseExact(f1, "MM/dd/yyyy", CultureInfo.InvariantCulture));
            DateTime EndDate =
                Convert.ToDateTime(DateTime.ParseExact(f2, "MM/dd/yyyy", CultureInfo.InvariantCulture));           
           
            if (res.Count > 0)
            {
                list = new List<ShowInformationVm>();
                var gridData = res.Where(x => Convert.ToDateTime(DateTime.ParseExact(x.CreateDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)) >= StartDate).Where(x => Convert.ToDateTime(DateTime.ParseExact(x.CreateDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)) <= EndDate);
                list = gridData.ToList();                
            }

            return Json(new { list });
        }

        private string GetModifiedDate(string date)
        {
            int firstIndex = date.IndexOf("/");
            int lastIndex = date.LastIndexOf("/");
            var month = Convert.ToInt32(date.Substring(0, firstIndex));
            int day;
            if (month < 10)
            {
                day = Convert.ToInt32(date.Substring(firstIndex + 1, lastIndex - 2));
            }
            else
            {
                day = Convert.ToInt32(date.Substring(firstIndex + 1, lastIndex - 3));
            }
            string yyyy = date.Substring(lastIndex + 1, 4);
            string dd = day < 10 ? "0" + day.ToString() : day.ToString();
            string mm = month < 10 ? "0" + month.ToString() : month.ToString();
            string dateString = mm + "/" + dd + "/" + yyyy;
            return dateString;
        }
    }
}
