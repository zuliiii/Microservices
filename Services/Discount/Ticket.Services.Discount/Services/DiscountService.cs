using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Ticket.Shared.DTOs;

namespace Ticket.Services.Discount.Services
{
    public class DiscountService : IDiscountService  
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;

            _dbConnection = new SqlConnection(_configuration.GetConnectionString("SqlServer"));

        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", StatusCodes.Status404NotFound);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("Select * from discount");

            return Response<List<Models.Discount>>.Success(discounts.ToList(), StatusCodes.Status200OK);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount where userid=@UserId and code=@Code", new { UserId = userId, Code = code });

            var hasDiscount = discounts.FirstOrDefault();

            return hasDiscount ==null ? Response<Models.Discount>.Fail("Discount not found", StatusCodes.Status404NotFound) : Response<Models.Discount>.Success(hasDiscount, StatusCodes.Status200OK);

        
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id=@Id", new { Id = id })).SingleOrDefault();

            return discount == null ? Response<Models.Discount>.Fail("Discount not found", StatusCodes.Status404NotFound) : Response<Models.Discount>.Success(discount, StatusCodes.Status200OK);


        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);

            return saveStatus > 0 ? Response<NoContent>.Success(StatusCodes.Status204NoContent) : Response<NoContent>.Fail("an error occurred while adding", StatusCodes.Status500InternalServerError);

        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id", new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });

            return status > 0 ? Response<NoContent>.Success(StatusCodes.Status204NoContent) : Response<NoContent>.Fail("Discount not found", StatusCodes.Status404NotFound);

        }
    }
}
