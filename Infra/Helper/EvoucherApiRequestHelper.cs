
using Infra.Services;
//using PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using X.PagedList;
using Data.ViewModels;
namespace Infra.Helper
{
    public class EvoucherApiRequestHelper
    {
        public static async Task<PagedListClient<evoucher>> evoucherlist(string token, int pagesize = 0,int page=0)
        {
            string url = $"api/evoucher/list?pagesize="+pagesize+"&page="+page;
            var data = await ApiRequest<PagedListServer<evoucher>>.GetRequest(url,token);
            var model = new PagedListClient<evoucher>();
            var pagedList = new StaticPagedList<evoucher>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
           
        }
        public static async Task<evoucher> IsActive(int id,string token)
        {
            string url = $"api/evoucher/IsActive?id="+id;
            var data = await ApiRequest<evoucher>.GetRequest(url,token);

            return data;
        }
        public static async Task<evoucher> Upsert(evoucher evoucherdata,string token)
        {
            string url = $"api/evoucher/Upsert";
            var data = await ApiRequest<evoucher>.PostRequest(url, token,evoucherdata);

            return data;
        }
        public static async Task<evoucher> Edit(int id,string token)
        {
            string url = $"api/evoucher/Edit?id="+id;
            var data = await ApiRequest<evoucher>.GetRequest(url,token);

            return data;
        }
        public static async Task<CheckOutViewModel> CheckoutUpsert(CheckOutViewModel checkout,string token)
        {
            string url = $"api/evoucher/CheckoutUpsert";
            var data = await ApiRequest<CheckOutViewModel>.PostRequest(url, token, checkout);

            return data;
        }

        //public static async Task<PagedListClient<CarWebViewModel>> _searchcarList(int PageSize = 20, int Page = 0, string BrandName = null, string ModelName = null, int Year = 0,string Cartype=null, string Type = null,
        // string SteeringPlace = null, string FuelType = null, string Engine = null,
        //    decimal MinPrice = 0, decimal MaxPrice = 0, int MinMileage = 0, int MaxMileage = 0, string memberid = null)
        //{
        //    string url = $"api/buy/searchCarResult?PageSize={PageSize}&Page={Page}&BrandName={BrandName}&ModelName={ModelName}&Year={Year}&Type={Type}&Cartype={Cartype}&SteeringPlace={SteeringPlace}&FuelType={FuelType}&Engine={Engine}&MinPrice={MinPrice}&MaxPrice={MaxPrice}&MinMileage={MinMileage}&MaxMileage={MaxMileage}&memberid={memberid}";
        //    var data = await ApiRequest<PagedListServer<CarWebViewModel>>.GetRequest(url);
        //    var model = new PagedListClient<CarWebViewModel>();
        //    var pagedList = new StaticPagedList<CarWebViewModel>(data.Results, Page, PageSize, data.TotalCount);
        //    model.Results = pagedList;
        //    model.TotalCount = data.TotalCount;
        //    model.TotalPages = data.TotalPages;
        //    return model;
        //}
        //public static async Task<PriceLogs> getMinMaxPriceMMK()
        //{
        //    string url = $"api/buy/getMinMaxPriceMMK";
        //    return await ApiRequest<PriceLogs>.GetRequest(url);
        //}
        //public static async Task<Advertisements> GetTopBanner()
        //{

        //    string url = $"api/buy/GetTopBanner";
        //    var model = await ApiRequest<Advertisements>.GetRequest(url);

        //    return model;

        //}
        //public static async Task<List<Advertisements>> GetBottomBanner()
        //{

        //    string url = $"api/buy/GetBottomBanner";
        //    var model = await ApiRequest<List<Advertisements>>.GetRequest(url);

        //    return model;

        //}

    }
}
