using System;
using Microsoft.EntityFrameworkCore;

public interface IFollowerRepository 
{
    DbContext Context { get; }
}