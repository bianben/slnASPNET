﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjASPNET.Models
{
    public class CShoppingCartItem
    {
        public int productId {  get; set; }
        public decimal price {  get; set; }
        public int count {  get; set; }
        public decimal 小記 { get { return this.count * this.price; } }
        public tProduct product { get; set; }
    }
}