﻿namespace Backend.DTOs.Beer;

public class BeerInsertDto
{
    public string Name { get; set; }
    public int BrandId { get; set; }
    public decimal Alcohol { get; set; }
}