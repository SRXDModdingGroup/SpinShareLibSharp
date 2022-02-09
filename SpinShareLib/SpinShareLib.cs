using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using SpinShareLib.Types;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SpinShareLib
{
    public class SSAPI
    {
        public string apiBase { get; private set; }
        public int supportedVersion { get; private set; }

        public HttpClient client { get; private set; }
        
        public SemaphoreSlim semaphore { get; private set; }

        public SSAPI() : this(new HttpClient()) { }
        public SSAPI(HttpClient client)
        {
            this.apiBase = "https://spinsha.re/api/";
            this.supportedVersion = 1;
            this.client = client;
            semaphore = new SemaphoreSlim(1);
        }
        
        public async Task<Content> ping()
        {
            return await this.getApiResultAsType<Content>($"{this.apiBase}ping");
        }
        public async Task<Content<Promo[]>> getPromos()
        {
            return await this.getApiResultAsType<Content<Promo[]>>($"{this.apiBase}promos");
        }
        public async Task<Content<Song[]>> getNewSongs(int offset)
        {
            return await this.getApiResultAsType<Content<Song[]>>($"{this.apiBase}songs/new/{offset}");
        }
        public async Task<Content<Song[]>> getHotThisWeekSongs(int offset)
        {
            return await this.getApiResultAsType<Content<Song[]>>($"{this.apiBase}songs/hotThisWeek/{offset}");
        }
        public async Task<Content<SongDetailTournament[]>> getTournamentMapPool()
        {
            return await this.getApiResultAsType<Content<SongDetailTournament[]>>($"{this.apiBase}tournament/mappool");
        }
        public async Task<Content<SongDetail>> getSongDetail(string songId)
        {
            return await this.getApiResultAsType<Content<SongDetail>>($"{this.apiBase}song/{songId}");
        }
        public async Task<(HttpResponseMessage response, Content<SongDetail> songdetail)> downloadSongZipStream(string songId)
        {
            var song = await getSongDetail(songId);
            return (await client.GetAsync(song.data.paths.zip), song);
        }
        public async Task<bool> downloadSongZip(string songId, string directoryPath)
        {
            try
            {
                var tup = await downloadSongZipStream(songId);
                using (var fs = new FileStream(Path.Combine(directoryPath, $"{tup.songdetail.data.fileReference}.srtb"), FileMode.CreateNew))
                {
                    await tup.response.Content.CopyToAsync(fs);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> downloadSongZipAddToQueue(string songId, string directoryPath)
        {
            await semaphore.WaitAsync();
            try
            {
                return await downloadSongZip(songId, directoryPath);
            }
            finally
            {
                semaphore.Release();
            }
        }
        public async Task<Content<Reviews>> getSongDetailReviews(string songId)
        {
            return await this.getApiResultAsType<Content<Reviews>>($"{this.apiBase}song/{songId}/reviews");
        }
        public async Task<Content<SpinPlays>> getSongDetailSpinPlays(string songId)
        {
            return await this.getApiResultAsType<Content<SpinPlays>>($"{this.apiBase}song/{songId}/spinplays");
        }
        public async Task<Content<Playlist>> getPlaylist(string playlistId)
        {
            return await this.getApiResultAsType<Content<Playlist>>($"{this.apiBase}playlist/{playlistId}");
        }
        public async Task<Content<UserDetail>> getUserDetail(string userId)
        {
            return await this.getApiResultAsType<Content<UserDetail>>($"{this.apiBase}user/{userId}");
        }
        public async Task<Content<Song[]>> getUserCharts(string userId)
        {
            return await this.getApiResultAsType<Content<Song[]>>($"{this.apiBase}user/{userId}/charts");
        }
        public async Task<Content<Reviews.Review[]>> getUserReviews(string userId)
        {
            return await this.getApiResultAsType<Content<Reviews.Review[]>>($"{this.apiBase}user/{userId}/reviews");
        }
        public async Task<Content<SpinPlays.Spinplay[]>> getUserSpinPlays(string userId)
        {
            return await this.getApiResultAsType<Content<SpinPlays.Spinplay[]>>($"{this.apiBase}user/{userId}/spinplays");
        }
        public async Task<Content<Playlist[]>> getUserPlaylists(string userId)
        {
            return await this.getApiResultAsType<Content<Playlist[]>>($"{this.apiBase}user/{userId}/playlists");
        }
        public async Task<Content<Search>> search(string query)
        {
            return await this.getApiResultAsType<Content<Search>>($"{this.apiBase}search/{query}");
        }
        public async Task<Content<Search>> searchAll()
        {
            return await this.getApiResultAsType<Content<Search>>($"{this.apiBase}searchAll");
        }
        private async Task<T> getApiResultAsType<T>(string apiPath)
        {
            var resp = await client.GetAsync(apiPath);
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var options = new JsonSerializerOptions
                {
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true,
                    IncludeFields = true,
                    Converters = { new DateTimeParse() }
                };
                return JsonSerializer.Deserialize<T>(await resp.Content.ReadAsStringAsync(), options);
                // return JsonConvert.DeserializeObject<T>(await resp.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }
        }
    }
}
