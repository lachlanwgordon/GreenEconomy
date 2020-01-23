using System;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.ViewModels;

namespace GreenEconomy.ViewModels
{
    public class ItemDetailViewModel : ViewModelBase
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
