
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderMinimalApi.Commands;
using OrderMinimalApi.Data;
using OrderMinimalApi.Models;
using OrderMinimalApi.Queries;

namespace OrderMinimalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ReadDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("ReadDatabase"));
            });
            builder.Services.AddDbContext<WriteDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("WriteDatabase"));
            });

            builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidation>();
            builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssembly(typeof(Program).Assembly));



            // Add services to the container.

            //builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //app.UseHttpsRedirection();

            //app.UseAuthorization();


            //app.MapControllers();

            app.MapPost("/api/order", async (IMediator _mediator, CreateOrderCommand command) =>
            {
                try
                {
                    var CreatedOrder =await _mediator.Send(command);

                    if (CreatedOrder != null)
                        return Results.Created($"/api/order/{CreatedOrder.Id}", CreatedOrder);
               
                    return Results.BadRequest("Failed to create order");

                }
                catch(ValidationException ex)
                {
                    var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    return Results.BadRequest(errors);

                }

            });

            app.MapGet("/api/order/{id}",async (IMediator _mediator, int id) => 
            {
                var order = await _mediator.Send(new GetOrderByIdQuery(id));
                if (order != null)
                    return Results.Ok(order);
                return Results.NotFound();

            });

            app.MapGet("/api/orders", async (IMediator _mediator ) =>
            {
                var orders = await _mediator.Send(new GetOrderSummariesQuery());
                if (orders != null)
                    return Results.Ok(orders);
                return Results.NotFound();

            });

            app.Run();
        }
    }
}
