using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.DTOs.ChatHistoryDTOs;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;

namespace PawFund.Infrastructure.Dapper.Repositories;

public class ChatHistoryRepository : IChatHistoryRepository
{
    private readonly IConfiguration _configuration;

    public ChatHistoryRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public async Task<int> AddAsync(ChatHistory entity)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var sql = @"INSERT INTO ChatHistories
                ([Id], [UserId], [ChatPartnerId], [Read], [Content], [CreatedDate], [ModifiedDate], [IsDeleted])
                VALUES (@Id, @UserId, @ChatPartnerId, @Read, @Content, @CreatedDate, @ModifiedDate, @IsDeleted)";

            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(sql, entity);
            
            return result;
        }
    }

    public Task<int> DeleteAsync(ChatHistory entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ChatHistory>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ChatHistory>? GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<ChatHistory> GetChatSenderAndRecieverHistoryAsync(CreateChatHistoryDTO createChatHistoryDto)
    {

        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var sql = @"SELECT [Id]
                      ,[UserId]
                      ,[ChatPartnerId]
                      ,[Read]
                      ,[CreatedDate]
                      ,[ModifiedDate]
                      ,[IsDeleted]
                      FROM ChatHistories 
                      WHERE UserId = @UserId 
                        AND ChatPartnerId = @ChatPartnerId";
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<ChatHistory>(sql, new
            {
                UserId = createChatHistoryDto.UserId,
                ChatPartnerId = createChatHistoryDto.ChatPartnerId
            });
            return result;
        }
    }

    public async Task<List<ChatHistory>> GetUserNeedSupportAsync(Guid userStaffId)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var sql = @"
            SELECT ch.UserId, ch.ChatPartnerId, ch.[Read], ch.Content, ch.ModifiedDate, a.FirstName, a.LastName, a.CropAvatarUrl
            FROM ChatHistories ch
            JOIN Accounts a ON ch.UserId = a.Id
            WHERE ch.UserId <> @ExcludedUserId
            ORDER BY ch.[Read] ASC, ch.ModifiedDate DESC";

            await connection.OpenAsync();

            var result = await connection.QueryAsync<ChatHistory, Account, ChatHistory>(
                sql,
                (chatHistory, account) =>
                {
                    chatHistory.User = account;
                    return chatHistory;
                },
                new { ExcludedUserId = userStaffId },
                splitOn: "FirstName"
            );

            return result.ToList();
        }
    }


    public async Task<int> UpdateAsync(ChatHistory entity)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
        {
            var sql = @"UPDATE ChatHistories 
                SET [UserId] = @UserId,
                    [ChatPartnerId] = @ChatPartnerId,
                    [Read] = @Read,
                    [Content] = @Content,
                    [ModifiedDate] = @ModifiedDate
                WHERE Id = @Id";

            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(sql, entity);

            return result;
        }
    }
}
