using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HekaMiniumApi.Context;
using System;
public static class SchemaFactory {
    public static string ConnectionString { get; set; } = "";
    public static HekaMiniumSchema CreateContext() {
        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseNpgsql(ConnectionString);
        HekaMiniumSchema nodeContext = new HekaMiniumSchema(optionsBuilder.Options);
        return nodeContext;
    }

    public static void ApplyMigrations(){
        var nodeContext = CreateContext();
        if (nodeContext != null){
            try
            {
                nodeContext.Database.Migrate();
                nodeContext.Dispose();

                Console.WriteLine("Migration Succeeded");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Migration Error: " + ex.Message);
            }
        }
    }
}