using System;
using Microsoft.EntityFrameworkCore;
using MyApp.Entities;

public interface IUserRepository 
{
    Context Context { get; }
}