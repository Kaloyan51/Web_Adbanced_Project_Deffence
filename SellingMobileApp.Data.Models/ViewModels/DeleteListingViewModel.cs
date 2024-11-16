using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellingMobileApp.Data.Models.ViewModels
{
    public class DeleteListingViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int OwnerNameUser { get; set; }
    }
}
