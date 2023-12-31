﻿namespace SalesSystem.Shared.DTOs
{
    public class ReportDTO
    {
        public string? DocumentNumber { get; set; }
        public string? PaymentType { get; set; }
        public string? CreatedDate { get; set; }
        public string? SaleTotal { get; set; }
        public string? Product { get; set; }
        public int? Quantity { get; set; }
        public string? Price { get; set; }
        public string? Total { get; set; }
    }
}
