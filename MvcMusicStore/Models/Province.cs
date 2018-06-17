using System;
using System.Collections.Generic;

namespace MvcMusicStore.Models
{
    public partial class Province
    {
        public Province()
        {
            Order = new HashSet<Order>();
        }

        public string ProvinceCode { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }

        public Country CountryCodeNavigation { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
