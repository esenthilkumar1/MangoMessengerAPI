﻿using System.Threading;
using System.Threading.Tasks;
using MangoAPI.BusinessLogic.ApiCommands.PasswordRestoreRequests;
using MangoAPI.IntegrationTests.Helpers;
using Xunit;

namespace MangoAPI.IntegrationTests.ApiCommandsTests.RequestPasswordRestoreCommandHandlerTests;

public class RequestPasswordRestoreTestSuccess : IntegrationTestBase
{
    private readonly Assert<RequestPasswordRestoreResponse> _assert = new();

    [Fact]
    public async Task RequestPasswordRestoreTest_Success()
    {
        await MangoModule.RequestAsync(
            request: CommandHelper.RegisterPetroCommand(),
            cancellationToken: CancellationToken.None);

        var result = await MangoModule.RequestAsync(
            request: CommandHelper.CreateRequestPasswordRestoreCommand("kolosovp95@gmail.com"),
            cancellationToken: CancellationToken.None);

        _assert.Pass(result);
    }
}