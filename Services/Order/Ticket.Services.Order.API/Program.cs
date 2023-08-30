using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Ticket.Services.Order.Application.Consumers;
using Ticket.Services.Order.Application.Handlers;
using Ticket.Services.Order.Infrastructure;
using Ticket.Shared.Services;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddMassTransit(x =>
		{
			x.AddConsumer<CreateOrderMessageCommandConsumer>();
			x.AddConsumer<EventNameChangedEventConsumer>();
			// Default Port : 5672
			x.UsingRabbitMq((context, cfg) =>
			{
				cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
				{
					host.Username("guest");
					host.Password("guest");
				});
				cfg.ReceiveEndpoint("create-order-service", e =>
				{
					e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
				});
				cfg.ReceiveEndpoint("event-name-changed-event-order-service", e =>
				{
					e.ConfigureConsumer<EventNameChangedEventConsumer>(context);
				});

			});
		});

		builder.Services.AddMassTransitHostedService();

		// Add services to the container.



		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		builder.Services.AddDbContext<OrderDbContext>(opt =>
		{
			opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
			{
				configure.MigrationsAssembly("Ticket.Services.Order.Infrastructure");
			});
		});

		var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

		JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.Authority = builder.Configuration["IdentityServerURL"];
				options.Audience = "resource_order";
				options.RequireHttpsMetadata = false;

			});

		builder.Services.AddControllers(opt =>
		{
			opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
		});

		builder.Services.AddHttpContextAccessor();
		builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

		builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateOrderCommandHandler)));

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}