﻿using MediatR;
using YoutubeLinks.Api.Auth;
using YoutubeLinks.Api.Data.Repositories;
using YoutubeLinks.Api.Extensions;
using YoutubeLinks.Api.Features.Links.Extensions;
using YoutubeLinks.Shared.Abstractions;
using YoutubeLinks.Shared.Exceptions;
using YoutubeLinks.Shared.Features.Links.Queries;
using YoutubeLinks.Shared.Features.Links.Responses;

namespace YoutubeLinks.Api.Features.Links.Queries
{
    public static class GetAllLinksFeature
    {
        public static IEndpointRouteBuilder Endpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/links/all", async (
                GetAllLinks.Query query,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                return Results.Ok(await mediator.Send(query, cancellationToken));
            })
                .WithTags("Playlists")
                .AllowAnonymous();

            return app;
        }

        public class Handler : IRequestHandler<GetAllLinks.Query, PagedList<LinkDto>>
        {
            private readonly ILinkRepository _linkRepository;
            private readonly IPlaylistRepository _playlistRepository;
            private readonly IAuthService _authService;

            public Handler(
                ILinkRepository linkRepository,
                IPlaylistRepository playlistRepository,
                IAuthService authService)
            {
                _linkRepository = linkRepository;
                _playlistRepository = playlistRepository;
                _authService = authService;
            }

            public async Task<PagedList<LinkDto>> Handle(
                GetAllLinks.Query query,
                CancellationToken cancellationToken)
            {
                var playlist = await _playlistRepository.Get(query.PlaylistId) ?? throw new MyNotFoundException();

                var isUserPlaylist = _authService.IsLoggedInUser(playlist.UserId);
                var linkQuery = _linkRepository.AsQueryable(query.PlaylistId, isUserPlaylist);

                linkQuery = linkQuery.FilterLinks(query);
                linkQuery = linkQuery.SortLinks(query);

                var linksPagedList = PageListExtensions<LinkDto>.Create(linkQuery.Select(x => x.ToDto()),
                                                                                  query.Page,
                                                                                  query.PageSize);

                return linksPagedList;
            }
        }
    }
}
