using eCommerce.Entities;

namespace eCommerce.Shared.Helpers
{
    public class EmailTextHelpers
    {
        private readonly ConfigurationsHelper _configurationsHelper;

        public EmailTextHelpers(ConfigurationsHelper configurationsHelper)
        {
            _configurationsHelper = configurationsHelper;
        }

        public string AccountRegisterEmailSubject(int languageID)
        {
            return $"{_configurationsHelper.ApplicationName} Account Registered!";
        }

        public string AccountRegisterEmailBody(int languageID, string loginURL)
        {
            return $"Thanks for registering your account on {_configurationsHelper.ApplicationName}. Your account has been created successfully. You can login to your account here: {loginURL}";
        }

        public string OrderPlacedEmailSubject(int languageID, int orderID)
        {
            return $"Order Placed Successfully. Order# {orderID}";
        }

        public string OrderPlacedEmailBody(int languageID, int orderID, string orderTrackingURL)
        {
            return $"Your order# {orderID} has been placed successfully on {_configurationsHelper.ApplicationName}. You can check the details of your order here: {orderTrackingURL}. You will be updated with your order status.";
        }

        public string OrderPlacedEmailSubject_Admin(int languageID, int orderID)
        {
            return $"New Order# {orderID} has been Placed on {_configurationsHelper.ApplicationName}.";
        }

        public string OrderPlacedEmailBody_Admin(int languageID, int orderID, string orderDetailsURL)
        {
            return $"A new order# {orderID} has been placed successfully on {_configurationsHelper.ApplicationName}. You can check the details of the order here: {orderDetailsURL}.";
        }

        public string OrderStatusUpdatedEmailSubject(int languageID, int orderID, int orderStatus)
        {
            return $"Order# {orderID} Status updated to {(OrderStatus)orderStatus}.";
        }

        public string OrderStatusUpdatedEmailBody(int languageID, int orderID, int orderStatus, string orderTrackingURL)
        {
            return $"Your order# {orderID} status has been updated to {(OrderStatus)orderStatus} on {_configurationsHelper.ApplicationName}. You can check the details of your order here: {orderTrackingURL}.";
        }

        public string ContactMessageSubject_Admin()
        {
            return $"A new contact us message has been received on {_configurationsHelper.ApplicationName}";
        }

        public string ContactMessageBody_Admin(string subject, string name, string email, string message)
        {
            return $"A new message has been received on {_configurationsHelper.ApplicationName}. Following are the details. <br> Subject: {subject}<br> Name: {name}<br> Email: {email}<br> Message Details: {message}";
        }
    }
}
