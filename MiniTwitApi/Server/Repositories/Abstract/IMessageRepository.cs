using System;
using Microsoft.EntityFrameworkCore;

public interface IMessageRepository 
{
    DbContext Context { get; }
}