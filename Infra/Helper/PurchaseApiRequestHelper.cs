using Data.Models;
using Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Infra.Helper
{
   public class PurchaseApiRequestHelper
    {
        public static async Task<PagedListClient<purchase>> purchaselist(string token, int pagesize = 0, int page = 0)
        {
            string url = $"api/purchase/list?pagesize=" + pagesize + "&page=" + page;
            var data = await ApiRequest<PagedListServer<purchase>>.GetRequest(url, token);
            var model = new PagedListClient<purchase>();
            var pagedList = new StaticPagedList<purchase>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;

        }
        public static async Task<purchase> checkvoucher(int id=0,string token=null)
        {
            string url = $"api/purchase/checkvoucher?id=" + id;
            var data = await ApiRequest<purchase>.GetRequest(url, token);

            return data;
        }
    }
}
