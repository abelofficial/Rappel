using API.Application.Profiles;
using API.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Tests;

public abstract class BaseHandlerTest
{

    public readonly IMapper _mapper;

    protected BaseHandlerTest(DbContextOptions<AppDbContext> context)
    {
        ContextOptions = context;
        Seed();

        var mapperConfig = new MapperConfiguration(c =>
         {
             c.AddProfile<CommandToEntity>();
             c.AddProfile<EntityToResult>();
         });

        _mapper = mapperConfig.CreateMapper();
    }

    protected DbContextOptions<AppDbContext> ContextOptions { get; }

    private void Seed()
    {
        using (var context = new AppDbContext(ContextOptions))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var adminUser = context.Add(MockUsers.GetAdminUser()).Entity;
            var uniqueUser = context.Add(MockUsers.GetUniqueUser(1)).Entity;

            context.SaveChanges();

        }
    }



}