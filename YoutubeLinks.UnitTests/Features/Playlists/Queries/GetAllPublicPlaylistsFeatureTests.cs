﻿using FluentAssertions;
using MediatR;
using NSubstitute;
using YoutubeLinks.Api.Data.Entities;
using YoutubeLinks.Api.Data.Repositories;
using YoutubeLinks.Api.Features.Playlists.Queries;
using YoutubeLinks.Shared.Abstractions;
using YoutubeLinks.Shared.Features.Playlists.Queries;
using YoutubeLinks.Shared.Features.Playlists.Responses;

namespace YoutubeLinks.UnitTests.Features.Playlists.Queries;

public class GetAllPublicPlaylistsFeatureTests
{
    [Fact]
    public async Task GetAllPublicPlaylistsHandler_ReturnsPlaylistsPagedList()
    {
        var query = new GetAllPublicPlaylists.Query
        {
            Page = 1,
            PageSize = 10,
            SortColumn = "name",
            SortOrder = SortOrder.Ascending,
            SearchTerm = ""
        };

        var list = new List<Playlist>
        {
            new()
            {
                Id = 1
            }
        };

        var playlistRepository = Substitute.For<IPlaylistRepository>();
        var mediator = Substitute.For<IMediator>();

        playlistRepository.AsQueryablePublic().Returns(list.AsQueryable());

        mediator.Send(Arg.Any<GetAllPublicPlaylists.Query>(), CancellationToken.None)
            .Returns(callInfo =>
            {
                var handler = new GetAllPublicPlaylistsFeature.Handler(playlistRepository);
                return handler.Handle(callInfo.Arg<GetAllPublicPlaylists.Query>(), CancellationToken.None);
            });

        var result = await mediator.Send(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<PagedList<PlaylistDto>>();
        result.TotalCount.Should().Be(1);
        result.Items.Count.Should().Be(1);
    }
}