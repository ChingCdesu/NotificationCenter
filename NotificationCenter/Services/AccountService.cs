using Microsoft.AspNetCore.Mvc;
using NotificationCenter.Storage;

namespace NotificationCenter.Services;

public class AccountService(DatabaseContext database)
{
    private readonly DatabaseContext _database = database;
}