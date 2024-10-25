﻿using MediatR;
using YoutubeLinks.Api.Auth;
using YoutubeLinks.Api.Data.Repositories;
using YoutubeLinks.Api.Extensions;
using YoutubeLinks.Api.Features.Playlists.Extensions;
using YoutubeLinks.Api.Helpers;
using YoutubeLinks.Shared.Abstractions;
using YoutubeLinks.Shared.Features.Playlists.Queries;
using YoutubeLinks.Shared.Features.Playlists.Responses;

namespace YoutubeLinks.Api.Features.Playlists.Queries;

public static class GetAllUserPlaylistsFeature
{
    public static void Endpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/playlists/all", async (
                    GetAllUserPlaylists.Query query,
                    IMediator mediator,
                    CancellationToken cancellationToken)
                => Results.Ok(await mediator.Send(query, cancellationToken)))
            .WithTags(Tags.Playlists)
            .AllowAnonymous();
    }

    public class Handler : IRequestHandler<GetAllUserPlaylists.Query, PagedList<PlaylistDto>>
    {
        private readonly IAuthService _authService;
        private readonly IPlaylistRepository _playlistRepository;

        public Handler(
            IPlaylistRepository playlistRepository,
            IAuthService authService)
        {
            _playlistRepository = playlistRepository;
            _authService = authService;
        }

        public Task<PagedList<PlaylistDto>> Handle(
            GetAllUserPlaylists.Query query,
            CancellationToken cancellationToken)
        {
            var isUserPlaylist = _authService.IsLoggedInUser(query.UserId);
            var playlistQuery = _playlistRepository.AsQueryable(query.UserId, isUserPlaylist);

            playlistQuery = playlistQuery.FilterPlaylists(query);
            playlistQuery = playlistQuery.SortPlaylists(query);

            var playlistsPagedList = PageListExtensions<PlaylistDto>.Create(playlistQuery.Select(x => x.ToDto()),
                query.Page,
                query.PageSize);

            return Task.FromResult(playlistsPagedList);
        }
    }
}