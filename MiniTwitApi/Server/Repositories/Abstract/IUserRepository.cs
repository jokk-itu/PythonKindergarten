using System;
using Microsoft.EntityFrameworkCore;
public interface IUserRepository 
{
    DbContext Context { get; }
}