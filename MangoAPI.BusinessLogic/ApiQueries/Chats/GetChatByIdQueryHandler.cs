﻿using MangoAPI.BusinessLogic.BusinessExceptions;
using MangoAPI.BusinessLogic.Models;
using MangoAPI.DataAccess.Database;
using MangoAPI.DataAccess.Database.Extensions;
using MangoAPI.Domain.Constants;
using MangoAPI.Domain.Enums;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MangoAPI.BusinessLogic.ApiQueries.Chats
{
    public class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, GetChatByIdResponse>
    {
        private readonly MangoPostgresDbContext _postgresDbContext;

        public GetChatByIdQueryHandler(MangoPostgresDbContext postgresDbContext)
        {
            _postgresDbContext = postgresDbContext;
        }

        public async Task<GetChatByIdResponse> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _postgresDbContext.Users.FindUserByIdAsync(request.UserId, cancellationToken);

            if (user is null)
            {
                throw new BusinessException(ResponseMessageCodes.UserNotFound);
            }

            var chatEntity =
                await _postgresDbContext.Chats.FindChatByIdIncludeMessagesAsync(request.ChatId, cancellationToken);

            if (chatEntity is null)
            {
                throw new BusinessException(ResponseMessageCodes.ChatNotFound);
            }

            var userChat =
                await _postgresDbContext.UserChats.FindUserChatByIdAsync(request.UserId, request.ChatId,
                    cancellationToken);

            switch (userChat)
            {
                case null when chatEntity.ChatType == ChatType.DirectChat:
                    throw new BusinessException(ResponseMessageCodes.ChatNotFound);

                case null when chatEntity.ChatType == ChatType.PrivateChannel:
                    throw new BusinessException(ResponseMessageCodes.ChatNotFound);
            }

            var chat = new Chat
            {
                ChatId = chatEntity.Id,
                Title = chatEntity.Title,
                ChatType = chatEntity.ChatType,
                Image = chatEntity.Image,
                Description = chatEntity.Description,
                MembersCount = chatEntity.MembersCount,
                IsMember = userChat != null,
                IsArchived = userChat is { IsArchived: true },
                LastMessage = chatEntity.Messages.Any()
                    ? chatEntity.Messages.OrderBy(messageEntity => messageEntity.CreatedAt).Select(x => new Message
                    {
                        MessageId = x.Id,
                        UserDisplayName = x.User.DisplayName,
                        MessageText = x.Content,
                        CreatedAt = x.CreatedAt.ToShortTimeString(),
                        UpdatedAt = x.UpdatedAt?.ToShortTimeString(),
                        IsEncrypted = x.IsEncrypted,
                        AuthorPublicKey = x.AuthorPublicKey,
                        PictureUrl = x.User.PictureUrl,
                    }).Last()
                    : null,
            };

            return GetChatByIdResponse.FromSuccess(chat);
        }
    }
}