﻿namespace ETickets.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}