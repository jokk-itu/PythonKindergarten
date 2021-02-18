using System;
using Microsoft.EntityFrameworkCore;
using MyApp.Entities;

public interface IMessageRepository 
{
    Context Context { get; }
}