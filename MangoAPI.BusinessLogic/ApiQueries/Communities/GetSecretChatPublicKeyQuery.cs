﻿using System;
using MangoAPI.BusinessLogic.Responses;
using MediatR;

namespace MangoAPI.BusinessLogic.ApiQueries.Communities
{
    public record GetSecretChatPublicKeyQuery : IRequest<GenericResponse<GetSecretChatPublicKeyResponse>>
    {
        public Guid ChatId { get; init; }
        public Guid UserId { get; init; }
    }
}
