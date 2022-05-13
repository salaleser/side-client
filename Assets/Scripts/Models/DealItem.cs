using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class DealItem
    {
        public int id;
        public ItemItem item;
        public DealerItem buyer;
        public DealerItem seller;
        public int price;

        public override string ToString() => @$"Item Type: ""{item.type_title}"";
Buyer: ""{buyer.title}"" ({buyer.id});
Seller: ""{seller.title}"" ({seller.id});
Max Quantity: {item.quantity}
Price: <b>{price}</b>";
    }
}
