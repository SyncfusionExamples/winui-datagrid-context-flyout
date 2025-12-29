using System.ComponentModel;

namespace Context_Flyout
{
    public class OrderInfo : INotifyPropertyChanged
    {
        int orderID;
        int unitPrice;
        string customerId;
        string country;
        string customerName;
        string shippingCity;
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public OrderInfo()
        {
        }

        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; OnPropertyChanged("OrderID");}
        }

        public int UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; OnPropertyChanged("UnitPrice"); }
        }

        public string CustomerID
        {
            get { return customerId; }
            set { customerId = value; OnPropertyChanged("CustomerID"); }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; OnPropertyChanged("CustomerName"); }
        }

        public string Country
        {
            get { return country; }
            set { country = value; OnPropertyChanged("Country"); }
        }

        public string ShipCity
        {
            get { return shippingCity; }
            set { shippingCity = value; OnPropertyChanged("ShipCity"); }
        }

        public OrderInfo(int orderId, string customerName,int unitPrice, string country, string customerId, string shipCity)
        {
            OrderID = orderId;
            CustomerName = customerName;
            UnitPrice = unitPrice;
            Country = country;
            CustomerID = customerId;
            ShipCity = shipCity;
        }
    }
}
