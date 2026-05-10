using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Data_03.Entity
{
    public class Address
    {
        public int AddressId { get; set; }

        public string Location { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        //public int PersonId { get; set; }

        //public Person Person { get; set; }
    }
}
