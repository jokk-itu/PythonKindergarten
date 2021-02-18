using System;
using Microsoft.EntityFrameworkCore;
using MyApp.Entities;

public interface IFollowerRepository 
{
    Context Context { get; }
}