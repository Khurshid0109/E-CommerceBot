﻿namespace TelegramBot.Service.DTOs.Products;
public class ProductForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public string Image { get; set; }
    public string Provider { get; set; }
}
