using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly IConfiguration _configuration;

    public MessageRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<int> AddAsync(Message entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Message entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Message>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Message>? GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Message>> GetMessagesChatsAsync(Guid senderId, Guid receiverId)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var sql = @"
            SELECT [Id],
                   [SenderId],
                   [ReceiverId],
                   [Content],
                   [CreatedDate],
                   [ModifiedDate],
                   [IsDeleted]
            FROM [PawFund].[dbo].[Messages]
            WHERE 
                (SenderId = @SenderId AND ReceiverId = @ReceiverId) 
                OR (SenderId = @ReceiverId AND ReceiverId = @SenderId)";

            await connection.OpenAsync();

            var result = await connection.QueryAsync<Message>(
                sql,
                new { SenderId = senderId, ReceiverId = receiverId });

            return result.ToList();
        }
    }

    public Task<int> UpdateAsync(Message entity)
    {
        throw new NotImplementedException();
    }
}
