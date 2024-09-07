namespace Utility
{
    // SD stands for Static Details
    public class SD
    {
        public const string AdminRole = "Admin";
        public const string ShipperRole = "Shipper";
        public const string CustomerRole = "Customer";

        // For Stripe Payment Status
        public const string PaymentStatusPending = "Payment Pending";
        public const string PaymentStatusApproved = "Payment Approved";

        // For Order Status        
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";
    }
}
