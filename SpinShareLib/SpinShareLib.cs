using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using SpinShareLib.Types;

namespace SpinShareLib
{
    public class SSAPI
    {
        public string apiBase { get; private set; }
        public int supportedVersion { get; private set; }

        public HttpClient client { get; private set; }

        public SSAPI() : this(new HttpClient()) { }
        public SSAPI(HttpClient client)
        {
            this.apiBase = "https://spinsha.re/api/";
            this.supportedVersion = 1;
            this.client = client;
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
        public async Task<Content<Reviews>> getSongDetailReviews(string songId)
        {
            return await this.getApiResultAsType<Content<Reviews>>($"{this.apiBase}song/{songId}/reviews");
        }
        public async Task<Content<SpinPlays>> getSongDetailSpinPlays(string _songId)
        {
            return await this.getApiResultAsType<Content<SpinPlays>>($"{this.apiBase}song/{_songId}/spinplays");
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
                return JsonConvert.DeserializeObject<T>(await resp.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }
        }
    }
}
