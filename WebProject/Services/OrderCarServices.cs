﻿using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using WebProject.Models;
using WebProject.ViewModels;
using WebProject.WorkFunction;

namespace WebProject.Services
{
    public class OrderCarServices
    {
        private readonly FileIOFunction _fileIOFunction;
        private readonly GuestModelContext _context;
        private readonly ProductsService _productsService;

        public OrderCarServices(FileIOFunction fileIOFunction, GuestModelContext context, ProductsService productsService)
        {
            _fileIOFunction = fileIOFunction;
            _context = context;
            _productsService = productsService;
        }

        public async Task<VMOrderCar> GetOrderCarList(string acc, float offset = 0f)
        {
            List<string> OrderList = _fileIOFunction.ReadFileContent(acc);
            VMOrderCar ret = new VMOrderCar();
            ret.item = new List<VMOrderCarItem>();
            foreach (string line in OrderList)
            {
                string[] items = line.Split(",");
                VMOrderCarItem item = await GetVMOrderCarItemByItems(items, offset);
                if (item != null)
                    ret.item.Add(item);
            }
            return ret;
        }

        public async Task<VMOrderCarItem> GetVMOrderCarItemByItems(string[] item, float offset)
        {
            Product p = await _productsService.GetProductByID(item[0]);
            if (p == null)
                return null;
            VMOrderCarItem vMorderCarItem = ProductToVMOrderCarItem(p, item, offset);
            return vMorderCarItem;
        }

        public bool CheckSameItem(string fileName, string pid, int value, List<string> orderList)
        {
            bool isSame = false;
            for (int i = 0; i < orderList.Count(); i++)
            {
                string[] items = orderList[i].Split(",");
                if (items[0] == pid)
                {
                    int mValue = int.Parse(items[1]) + value;
                    string strValue = mValue.ToString();
                    orderList[i] = pid + "," + strValue;
                    return true;
                }
            }
            return false;
        }

        private VMOrderCarItem ProductToVMOrderCarItem(Product p, string[] item, float offset)
        {
            float price = p.CostJP * p.PriceExchangeRage;
            VMOrderCarItem vMorderCarItem = new VMOrderCarItem();
            vMorderCarItem.product = item[0];
            vMorderCarItem.price = (int)price;
            vMorderCarItem.img = p.Photo;
            vMorderCarItem.offset = offset;
            var count = int.Parse(item[1]);
            vMorderCarItem.count = count;
            vMorderCarItem.name = p.ProductName;

            return vMorderCarItem;
        }
    }
}
